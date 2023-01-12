using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace CBH.Core
{
    public class GameData
    {
        private int currentScene;

        private const string CompletedScenesPrefsName = "CompletedScenes";
        public const int CountLevels = 25;

        public int CurrentScene => currentScene;

        private void Start()
        {
            GetData();
        }

        private void GetData()
        {
            currentScene = PlayerPrefs.GetInt(CompletedScenesPrefsName, 0);
        }

        private void ResetGame()
        {
            currentScene = 0;
            PlayerPrefs.SetInt(CompletedScenesPrefsName, 0);
        }

        public void SaveGame(int index)
        {
            PlayerPrefs.SetInt(CompletedScenesPrefsName, index);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(currentScene + 1);
        }

        public void NewGame()
        {
            ResetGame();
            LoadGame();
        }
    }
}