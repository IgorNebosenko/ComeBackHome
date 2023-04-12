using System;

namespace CBH.Core.Levels
{
    public interface IUserLevelsInfo
    {
        int LastOpenedLevel { get; }
        int TotalLevels { get; }
        
        LevelsPrefsData GetDataAboutLevel(int id);
        bool IsLevelOpened(int id);

        void WriteResult(int levelId, TimeSpan result);
    }
}