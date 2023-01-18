using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CBH.Core.Entity
{
    public class RocketManager
    {
        private Coroutine timer;

        private GameData _gameData;
        private GameManager _gameManager;

        private float _localTimer;
        private bool _timerWasStart;

        public static UnityAction TimerStarter;
        private UnityAction LoaderLevel;

        public RocketState currentRocketState;

        public RocketManager(GameData gameData, GameManager gameManager)
        {
            _gameData = gameData;
            _gameManager = gameManager;
            
            _localTimer = 4f;
            _timerWasStart = false;
            currentRocketState = RocketState.Live;
            LoaderLevel = RocketLoadState;
            TimerStarter = StartTimer;
        }

        private void RocketLoadState()
        {
            _gameManager.HandleRocketState(currentRocketState);
        }

        private void StartTimer()
        {
            timer = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (_localTimer > 1)
            {
                ShowTextCounting();
                yield return new WaitForEndOfFrame();
                _localTimer -= Time.deltaTime;
            }

            _localTimer = 3;
            LoaderLevel.Invoke();
            yield return null;
        }
    }
}