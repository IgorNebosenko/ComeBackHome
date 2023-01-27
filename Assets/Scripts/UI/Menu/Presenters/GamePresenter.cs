using System;
using System.Collections;
using CBH.Core.Collision;
using CBH.Core.Entity.Input;
using CBH.UI.Views;
using ElectrumGames.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.UI.Presenters
{
    public class GamePresenter : Presenter<GameView>
    {
        private RocketController _rocketController;
        private FinishCollisionObject _landingPad;
        private Camera _mainCamera;
        
        private const int CountDirections = 4;
        private const float ClampModifier = 1.1f;

        public event Action<string> HeaderTextChanged;
        public event Action<string> TimerTextChanged; 

        public GamePresenter(GameView view, RocketController rocketController, Camera mainCamera,
            FinishCollisionObject landingPad) : base(view)
        {
            _rocketController = rocketController;
            _landingPad = landingPad;
            _mainCamera = mainCamera;

            view.Init(this);
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
    }
}