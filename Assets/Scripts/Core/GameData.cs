using UnityEngine;

namespace CBH.Core
{
    public class GameData
    {
        private int _lastCompletedScene;

        private const string TargetFpsPrefsName = "TargetFps";
        private int _targetFps;
        public int TargetFps => _targetFps;

        public GameData()
        {
            GetData();
        }

        private void GetData()
        {
            _targetFps = PlayerPrefs.GetInt(TargetFpsPrefsName, 30);
            Application.targetFrameRate = _targetFps;
        }

        public void UpdateTargetFps(int newFps)
        {
            _targetFps = newFps;
            PlayerPrefs.SetInt(TargetFpsPrefsName, _targetFps);
            Application.targetFrameRate = _targetFps;
        }
    }
}