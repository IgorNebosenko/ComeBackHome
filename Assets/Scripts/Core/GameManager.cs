using System;
using System.Collections;
using CBH.Ads;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core.Audio;
using CBH.Core.Entity;
using CBH.Core.IAP;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.Core
{
    public class GameManager
    {
        private GameData _gameData;
        private AdsData _adsData;
        private AdsConfig _adsConfig;
        private InputData _inputData;
        private IAdsProvider _adsProvider;
        private IStorePurchaseController _storePurchaseController;
        private IAnalyticsManager _analyticsManager;
        
        private bool _isLanded;
        private bool _isGameEnded;

        private float _timeAtStartLevel;
        
        private int _attemptsLanding;
        private float _timeStayLeft = LandingDuration;

        private const float RestartDuration = 3f;
        private const float RestartTickDuration = 1f;
        
        private const float LandingDuration = 3f;
        private const float BeforeWinDuration = 2f;
        
        public event Action<float> BeforeRestartLevel;
        public event Action<float> PlatformStay;
        public event Action BeforeWin;
        public event Action PlatformLeave;
        public event Action LevelLose;
        public event Action ReturnToMenu;

        public event Action<TimeSpan> UpdateFlyTime; 

        public RocketState CurrentState { get; private set; }
        public TimeSpan TimeFly { get; private set; } = TimeSpan.Zero;

        public GameManager(GameData gameData, AdsData adsData, AdsConfig adsConfig, InputData inputData, IAdsProvider adsProvider, 
            IStorePurchaseController storePurchaseController, IAnalyticsManager analyticsManager)
        {
            _gameData = gameData;
            _adsData = adsData;
            _adsConfig = adsConfig;
            _inputData = inputData;
            _adsProvider = adsProvider;
            _storePurchaseController = storePurchaseController;
            _analyticsManager = analyticsManager;

            CurrentState = RocketState.Live;
            _timeAtStartLevel = Time.realtimeSinceStartup;
        }

        public void HandleRocketState(RocketState state)
        {
            CurrentState = state;
            
            switch (state)
            {
                case RocketState.Dead:
                    AudioHandler.StopLoopSound();
                    _analyticsManager.SendEvent(new CrashPlayerRocketEvent(SceneManager.GetActiveScene().buildIndex,
                        _gameData.LastCompletedScene,
                        (float) TimeFly.TotalSeconds, _inputData, _attemptsLanding));
                    LevelLose?.Invoke();
                    Observable.FromCoroutine(RestartProcess).Subscribe();
                    break;
                case RocketState.LandFinishPad:
                    _analyticsManager.SendEvent(new LandingOnLandingPadEvent(SceneManager.GetActiveScene().buildIndex,
                        _gameData.LastCompletedScene, (float) TimeFly.TotalSeconds, ++_attemptsLanding));
                    Observable.FromCoroutine(LandingProcess).Subscribe();
                    break;
                case RocketState.LeaveFinishPad:
                    _analyticsManager.SendEvent(new LeaveLandingPadEvent(SceneManager.GetActiveScene().buildIndex,
                        _gameData.LastCompletedScene, (float) TimeFly.TotalSeconds, _attemptsLanding, _timeStayLeft));
                    PlatformLeave?.Invoke();
                    CurrentState = RocketState.Live;
                    break;
            }
        }

        public void StartFlyProcess()
        {
            var timeDifference = Time.realtimeSinceStartup - _timeAtStartLevel;
            _analyticsManager.SendEvent(new LaunchFromLaunchPadEvent(SceneManager.GetActiveScene().buildIndex, 
                _gameData.LastCompletedScene, timeDifference));
            Observable.FromCoroutine(UpdateFlyTimeProcess).Subscribe();
        }

        public void BackToMenu()
        {
            ReturnToMenu?.Invoke();
            Observable.FromCoroutine(ToMenuProcess).Subscribe();
        }

        private IEnumerator RestartProcess()
        {
            _isGameEnded = true;
            var timeLeft = RestartDuration;

            while (timeLeft >= 0f)
            {
                BeforeRestartLevel?.Invoke(timeLeft);
                yield return new WaitForSeconds(RestartTickDuration);
                timeLeft -= RestartTickDuration;
            }

            var isNeedAd = false;
            
            if (!_storePurchaseController.HasNoAdsSubscription)
            {
                _adsData.countRestartsFromLastAd++;
                _adsData.timeFlyFromLastAd += (float)TimeFly.TotalSeconds;

                if (_adsData.countRestartsFromLastAd >= _adsConfig.countRestartsBetweenAds ||
                    _adsData.timeFlyFromLastAd >= _adsConfig.timeFlyBetweenAds)
                {
                    isNeedAd = true;
                    _adsProvider.ShowInterstitial();
                    _adsData.Reset();
                }
            }

            _analyticsManager.SendEvent(new RestartLevelEvent(SceneManager.GetActiveScene().buildIndex,
                _gameData.LastCompletedScene, _adsData.timeFlyFromLastAd, _adsData.countRestartsFromLastAd, isNeedAd,
                _storePurchaseController.HasNoAdsSubscription));

            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            yield return SceneManager.LoadSceneAsync(currentSceneIndex);
        }
        
        private IEnumerator LandingProcess()
        {
            _timeStayLeft = LandingDuration;
            _isLanded = true;

            while (_timeStayLeft >= 0 && CurrentState == RocketState.LandFinishPad)
            {
                _timeStayLeft -= Time.deltaTime;
                PlatformStay?.Invoke(_timeStayLeft);
                yield return new WaitForEndOfFrame();
            }

            if (CurrentState == RocketState.LandFinishPad)
            {
                _isGameEnded = true;
                _analyticsManager.SendEvent(new LoadNextGameLevelEvent(SceneManager.GetActiveScene().buildIndex,
                    _gameData.LastCompletedScene, _adsData.timeFlyFromLastAd, _adsData.countRestartsFromLastAd));
                BeforeWin?.Invoke();
                
                _adsData.timeFlyFromLastAd += (float)TimeFly.TotalSeconds;
                
                yield return new WaitForSeconds(BeforeWinDuration);
                
                _analyticsManager.SendEvent(new SuccessfulLandingRocketEvent(SceneManager.GetActiveScene().buildIndex,
                    _gameData.LastCompletedScene,
                    (float) TimeFly.TotalSeconds, _inputData, _attemptsLanding));
                
                var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

                if (nextScene == SceneManager.sceneCountInBuildSettings)
                    nextScene = 1;

                _gameData.SaveGame(nextScene);
                yield return SceneManager.LoadSceneAsync(nextScene);
            }
            else
                _isLanded = false;
        }

        private IEnumerator UpdateFlyTimeProcess()
        {
            TimeFly = TimeSpan.Zero;

            while (!_isGameEnded)
            {
                yield return new WaitForEndOfFrame();
                if (_isLanded)
                    continue;
                
                TimeFly = TimeFly.Add(TimeSpan.FromSeconds(Time.deltaTime));
                UpdateFlyTime?.Invoke(TimeFly);
            }
        }
        
        private IEnumerator ToMenuProcess()
        {
            yield return SceneManager.LoadSceneAsync(0);
        }
    }
}