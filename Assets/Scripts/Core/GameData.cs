using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace CBH.Core
{
    public class GameData
    {
        private int _lastCompletedScene;

        private const string CompletedScenesPrefsName = "CompletedScenes";
        public const int CountLevels = 25;

        private const string TargetFpsPrefsName = "TargetFps";
        private int _targetFps;
        public int TargetFps => _targetFps;

        public int LastCompletedScene => _lastCompletedScene;

        public GameData()
        {
            GetData();
        }

        private void GetData()
        {
            _lastCompletedScene = PlayerPrefs.GetInt(CompletedScenesPrefsName, 1);
            
            _targetFps = PlayerPrefs.GetInt(TargetFpsPrefsName, 30);
            Application.targetFrameRate = _targetFps;
        }

        public void ResetGame()
        {
            _lastCompletedScene = 1;
            PlayerPrefs.SetInt(CompletedScenesPrefsName, 1);
        }

        public void SaveGame(int index)
        {
            if (_lastCompletedScene > index)
                return;

            _lastCompletedScene = index;
            PlayerPrefs.SetInt(CompletedScenesPrefsName, index);
        }

        public void UpdateTargetFps(int newFps)
        {
            _targetFps = newFps;
            PlayerPrefs.SetInt(TargetFpsPrefsName, _targetFps);
            Application.targetFrameRate = _targetFps;
        }
    }
}