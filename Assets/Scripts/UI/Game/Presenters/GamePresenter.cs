using System;
using System.Collections;
using CBH.Core;
using CBH.Core.Collision;
using CBH.Core.Entity.Input;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.UI.Game.Presenters
{
    public class GamePresenter : Presenter<GameView>
    {
        private RocketController _rocketController;
        private FinishCollisionObject _landingPad;
        private Camera _mainCamera;
        private GameManager _gameManager;
        
        private const int CountDirections = 4;
        private const float ClampModifier = 1.1f;

        public event Action<string> HeaderTextChanged;
        public event Action<string> TimerTextChanged; 

        public GamePresenter(GameView view, RocketController rocketController, Camera mainCamera, GameManager gameManager,
            FinishCollisionObject landingPad) : base(view)
        {
            _rocketController = rocketController;
            _landingPad = landingPad;
            _mainCamera = mainCamera;
            _gameManager = gameManager;

            view.Init(this);
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

        public void UpdateGps(RectTransform pointerTransform)
        {
            var distanceToPlayer = _landingPad.transform.position - _rocketController.ControllerPosition;
            var ray = new Ray(_rocketController.ControllerPosition, distanceToPlayer);

            var planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            var minDistance = Mathf.Infinity;

            for (var i = 0; i < CountDirections; i++)
            {
                if (!planes[i].Raycast(ray, out var distance))
                    continue;
                if (distance < minDistance)
                    minDistance = distance;
            }

            minDistance = Mathf.Clamp(minDistance - ClampModifier, 0, distanceToPlayer.magnitude);
            pointerTransform.position = _mainCamera.WorldToScreenPoint(ray.GetPoint(minDistance));
        }

        public void OnToMenuClicked()
        {
            Observable.FromCoroutine(ToMenuProcess).Subscribe();
        }

        private IEnumerator ToMenuProcess()
        {
            yield return SceneManager.LoadSceneAsync(0);
        }

        private void OnBeforeRestartLevel(float time)
        {
            if ((int)time > 0)
                HeaderTextChanged?.Invoke($"Oops. Restarting on {(int)time} seconds...");
            else
                HeaderTextChanged?.Invoke("Restart...");
        }

        private void OnPlatformStay(float time)
        {
            if ((int)time > 0)
                HeaderTextChanged?.Invoke($"Stay on platform for {(int)time} seconds");
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
                TimerTextChanged?.Invoke($"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds:00}");
        }
    }
}