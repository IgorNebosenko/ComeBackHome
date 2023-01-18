using System;
using CBH.Core.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.Core
{
    public class GameManager
    {
        private GameData _gameData;
        
        public event Action OnLoadNextLevel;
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
                    RestartLevel();
                    break;
                case RocketState.Win:
                    LoadNextLevel();
                    break;
            }
        }

        private void LoadNextLevel()
        {
            var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (nextScene == SceneManager.sceneCountInBuildSettings)
                nextScene = 0;
            
            OnLoadNextLevel?.Invoke();
            
            _gameData.SaveGame(nextScene);
            SceneManager.LoadScene(nextScene);
        }

        private void RestartLevel()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            OnRestartLevel?.Invoke();
            
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}