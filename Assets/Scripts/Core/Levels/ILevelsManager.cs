using CBH.Core.Configs.Levels;
using UnityEngine;

namespace CBH.Core.Levels
{
    public interface ILevelsManager
    {
        LevelDataPair CurrentLevel { get; }
        int CurrentLevelId { get; }
        
        AsyncOperation LoadTutorial();
        AsyncOperation LoadMenu();

        AsyncOperation ContinueGame();
        AsyncOperation LoadLevelByIndex(int id);
        AsyncOperation RestartLevel();
        AsyncOperation LoadLastLevel();
    }
}