using System;
using System.Collections;
using CBH.Core.Audio;
using CBH.Core.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.Core
{
    public class GameManager
    {
        private GameData _gameData;
        
        private bool _isLanded;
        private bool _isGameEnded;
        
        private const float RestartDuration = 3f;
        private const float RestartTickDuration = 1f;
        
        private const float LandingDuration = 3f;
        private const float BeforeWinDuration = 2f;
        
        public event Action<float> BeforeRestartLevel;
        public event Action RestartLevel;

        public event Action PlatformLand;
        public event Action<float> PlatformStay;
        public event Action BeforeWin;
        public event Action PlatformLeave;
        
        public event Action LevelWin;
        public event Action LevelLose;

        public event Action<TimeSpan> UpdateFlyTime; 

        public RocketState CurrentState { get; private set; }

        public GameManager(GameData gameData)
        {
            _gameData = gameData;
            CurrentState = RocketState.Live;
        }

        public void HandleRocketState(RocketState state)
        {
            CurrentState = state;
            
            switch (state)
            {
                case RocketState.Dead:
                    AudioHandler.StopLoopSound();
                    LevelLose?.Invoke();
                    Observable.FromCoroutine(RestartProcess).Subscribe();
                    break;
                case RocketState.LandFinishPad:
                    PlatformLand?.Invoke();
                    Observable.FromCoroutine(LandingProcess).Subscribe();
                    break;
                case RocketState.LeaveFinishPad:
                    PlatformLeave?.Invoke();
                    CurrentState = RocketState.Live;
                    break;
            }
        }

        public void StartFlyProcess()
        {
            Observable.FromCoroutine(UpdateFlyTimeProcess).Subscribe();
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
            
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            RestartLevel?.Invoke();
            yield return SceneManager.LoadSceneAsync(currentSceneIndex);
        }
        
        private IEnumerator LandingProcess()
        {
            _isLanded = true;
            var timeLeft = LandingDuration;

            while (timeLeft >= 0 && CurrentState == RocketState.LandFinishPad)
            {
                timeLeft -= Time.deltaTime;
                PlatformStay?.Invoke(timeLeft);
                yield return new WaitForEndOfFrame();
            }

            if (CurrentState == RocketState.LandFinishPad)
            {
                _isGameEnded = true;
                BeforeWin?.Invoke();
                yield return new WaitForSeconds(BeforeWinDuration);

                LevelWin?.Invoke();
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
            var time = TimeSpan.Zero;

            while (!_isGameEnded)
            {
                yield return new WaitForEndOfFrame();
                if (_isLanded)
                    continue;
                
                time = time.Add(TimeSpan.FromSeconds(Time.deltaTime));
                UpdateFlyTime?.Invoke(time);
            }
        }
    }
}