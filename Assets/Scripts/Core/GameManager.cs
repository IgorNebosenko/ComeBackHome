using System;
using System.Collections;
using CBH.Core.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.Core
{
    public class GameManager
    {
        private GameData _gameData;

        private const float RestartDuration = 3f;
        private const float RestartTickDuration = 1f;
        
        public event Action OnLoadNextLevel;
        public event Action<float> OnBeforeRestartLevel;
        public event Action OnRestartLevel;

        public GameManager(GameData gameData)
        {
            _gameData = gameData;
        }

        public void HandleRocketState(RocketState state)
        {
            switch (state)
            {
                case RocketState.Dead:
                    Observable.FromCoroutine(RestartProcess).Subscribe();
                    break;
                case RocketState.Win:
                    Observable.FromCoroutine(LoadNextLevelProcess).Subscribe();
                    break;
            }
        }

        private IEnumerator RestartProcess()
        {
            var timeLeft = RestartDuration;

            while (timeLeft > 0f)
            {
                OnBeforeRestartLevel?.Invoke(RestartTickDuration);
                yield return new WaitForSeconds(RestartTickDuration);
                timeLeft -= RestartTickDuration;
            }
            
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            OnRestartLevel?.Invoke();
            yield return SceneManager.LoadSceneAsync(currentSceneIndex);
        }

        private IEnumerator LoadNextLevelProcess()
        {
            var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (nextScene == SceneManager.sceneCountInBuildSettings)
                nextScene = 0;
            
            OnLoadNextLevel?.Invoke();
            
            _gameData.SaveGame(nextScene);
            yield return SceneManager.LoadSceneAsync(nextScene);
        }
    }
}