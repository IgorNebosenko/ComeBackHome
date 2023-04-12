using CBH.Core.Configs.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.Core.Levels
{
    public class LevelsManager : ILevelsManager, IHaveLevelsConfig
    {
        private readonly LevelsConfig _levelsConfig;
        private readonly IUserLevelsInfo _levelsInfo;

        private const int MenuSceneId = 0;
        private const int EmptyLevelId = -1;

        public LevelDataPair CurrentLevel { get; private set; } = null;
        public int CurrentLevelId { get; private set; }

        public LevelsManager(LevelsConfig levelsConfig, IUserLevelsInfo levelsInfo)
        {
            _levelsConfig = levelsConfig;
            _levelsInfo = levelsInfo;
        }

        public AsyncOperation LoadTutorial()
        {
            CurrentLevel = _levelsConfig.GetRandomTutorial();
            CurrentLevelId = EmptyLevelId;
            return SceneManager.LoadSceneAsync(CurrentLevel.buildIndex);
        }

        public AsyncOperation LoadMenu()
        {
            CurrentLevel = null;
            CurrentLevelId = EmptyLevelId;
            return SceneManager.LoadSceneAsync(MenuSceneId);
        }

        public AsyncOperation ContinueGame()
        {
            CurrentLevel = _levelsConfig.GetLevelByIndex(_levelsInfo.LastOpenedLevel);
            CurrentLevelId = _levelsInfo.LastOpenedLevel;
            return SceneManager.LoadSceneAsync(CurrentLevel.buildIndex);
        }

        public AsyncOperation LoadLevelByIndex(int id)
        {
            CurrentLevel = _levelsConfig.GetLevelByIndex(id);
            CurrentLevelId = id;
            return SceneManager.LoadSceneAsync(CurrentLevel.buildIndex);
        }

        public AsyncOperation RestartLevel()
        {
            return SceneManager.LoadSceneAsync(CurrentLevel.buildIndex);
        }

        public AsyncOperation LoadLastLevel()
        {
            CurrentLevel = null;
            CurrentLevelId = EmptyLevelId;
            return SceneManager.LoadSceneAsync(_levelsConfig.LastLevelBuildIndex);
        }
    }
}