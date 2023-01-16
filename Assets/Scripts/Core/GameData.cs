using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace CBH.Core
{
    public class GameData
    {
        private int _currentScene;

        private const string CompletedScenesPrefsName = "CompletedScenes";
        public const int CountLevels = 25;

        private const string TargetFpsPrefsName = "TargetFps";
        private int _targetFps;
        public int TargetFps => _targetFps;

        public int CurrentScene => _currentScene;

        public GameData()
        {
            GetData();
        }

        private void GetData()
        {
            _currentScene = PlayerPrefs.GetInt(CompletedScenesPrefsName, 0);
            
            _targetFps = PlayerPrefs.GetInt(TargetFpsPrefsName, 30);
            Application.targetFrameRate = _targetFps;
        }

        private void ResetGame()
        {
            _currentScene = 0;
            PlayerPrefs.SetInt(CompletedScenesPrefsName, 0);
        }

        public void SaveGame(int index)
        {
            PlayerPrefs.SetInt(CompletedScenesPrefsName, index);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(_currentScene + 1);
        }

        public void NewGame()
        {
            ResetGame();
            LoadGame();
        }

        public void UpdateTargetFps(int newFps)
        {
            PlayerPrefs.SetInt(TargetFpsPrefsName, newFps);
            Application.targetFrameRate = newFps;
        }
    }
}