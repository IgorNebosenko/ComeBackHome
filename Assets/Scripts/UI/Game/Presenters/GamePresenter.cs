using System;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.Collision;
using CBH.Core.Core.Misc;
using CBH.Core.Entity.Input;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CBH.UI.Game.Presenters
{
    public class GamePresenter : Presenter<GameView>
    {
        private IAnalyticsManager _analyticsManager;
        private RocketController _rocketController;
        private FinishCollisionObject _landingPad;
        private GameManager _gameManager;
        private GameData _gameData;
        private GlobalUserSettings _globalUserSettings;

        private const float distanceFullVisibleGPS = 50f;
        private const float distanceInvisibleGPS = 15f;
        private const float distanceDifferenceVisibility = distanceFullVisibleGPS - distanceInvisibleGPS;

        public event Action<string> HeaderTextChanged;
        public event Action<string> TimerTextChanged;

        public bool IsRightPositionBoost => _globalUserSettings.IsRightPositionBoost;

        public GamePresenter(IAnalyticsManager analyticsManager, RocketController rocketController, GameManager gameManager,
            FinishCollisionObject landingPad, GameData gameData, GlobalUserSettings globalUserSettings, GameView view) : 
                base(view)
        {
            _analyticsManager = analyticsManager;
            _rocketController = rocketController;
            _landingPad = landingPad;
            _gameManager = gameManager;
            _gameData = gameData;
            _globalUserSettings = globalUserSettings;
        }

        protected override void Init()
        {
            _gameManager.BeforeRestartLevel += OnBeforeRestartLevel;
            _gameManager.PlatformStay += OnPlatformStay;
            _gameManager.PlatformLeave += OnLeavePlatform;
            _gameManager.BeforeWin += OnBeforeWin;
            _gameManager.UpdateFlyTime += OnUpdateFlyTimer;
        }

        protected override void Closing()
        {
            _gameManager.BeforeRestartLevel -= OnBeforeRestartLevel;
            _gameManager.PlatformStay -= OnPlatformStay;
            _gameManager.PlatformLeave -= OnLeavePlatform;
            _gameManager.BeforeWin -= OnBeforeWin;
            _gameManager.UpdateFlyTime -= OnUpdateFlyTimer;
        }

        public void UpdateGps(Image arrowImage, RectTransform pivotTransform)
        {
            var distanceToPlayer = _landingPad.transform.position - _rocketController.ControllerPosition;

            var normalized = distanceToPlayer.normalized;
            pivotTransform.eulerAngles = Mathf.Atan2(normalized.x, normalized.y) * Mathf.Rad2Deg * Vector3.back;

            var distanceLength = distanceToPlayer.magnitude;
            
            if (distanceLength >= distanceFullVisibleGPS)
                arrowImage.color = Color.white;
            else
            {
                if (distanceLength <= distanceInvisibleGPS)
                    arrowImage.color = Color.clear;
                else
                {
                    distanceLength -= distanceInvisibleGPS;
                    arrowImage.color = new Color(1, 1, 1, distanceLength / distanceDifferenceVisibility);
                }
            }
        }

        public void OnToMenuClicked()
        {
            _analyticsManager.SendEvent(new ToMenuButtonPressedEvent(SceneManager.GetActiveScene().buildIndex, 
                _gameData.LastCompletedScene, (float)_gameManager.TimeFly.TotalSeconds));
            _gameManager.BackToMenu();
        }

        private void OnBeforeRestartLevel(float time)
        {
            var roundedTime = Mathf.Round(time);
            
            if (roundedTime > 0)
                HeaderTextChanged?.Invoke($"Oops. Restarting on {roundedTime} seconds...");
            else
                HeaderTextChanged?.Invoke("Restart...");
        }

        private void OnPlatformStay(float time)
        {
            var roundedTime = Mathf.Round(time);
            
            if (roundedTime > 0)
                HeaderTextChanged?.Invoke($"Stay on platform for {roundedTime} seconds");
            else
                OnBeforeWin();
        }

        private void OnLeavePlatform()
        {
            HeaderTextChanged?.Invoke("");
        }

        private void OnBeforeWin()
        {
            HeaderTextChanged?.Invoke("Successful landing! You win!");
        }

        private void OnUpdateFlyTimer(TimeSpan time)
        {
            if (time.TotalHours > 1)
                TimerTextChanged?.Invoke("More than 1 hour!");
            else
                TimerTextChanged?.Invoke($"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds / 10:00}");
        }
    }
}