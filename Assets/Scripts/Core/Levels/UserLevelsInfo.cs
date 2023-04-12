using System;
using CBH.Core.Configs.Levels;
using UnityEngine;

namespace CBH.Core.Levels
{
    public class UserLevelsInfo : IUserLevelsInfo, IHaveLevelsConfig
    {
        private readonly LevelsConfig _levelsConfig;
        
        private const string ObsoletePrefsName = "CompletedScenes";
        
        public int LastOpenedLevel { get; private set; }
        public int TotalLevels => _levelsConfig.CountGameLevels;

        public UserLevelsInfo(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
            
            Init();
        }

        public LevelsPrefsData GetDataAboutLevel(int id)
        {
            var result = PlayerPrefs.GetString(_levelsConfig.GetLevelByIndex(id).levelDataConfig.levelUniqueId);

            if (string.IsNullOrEmpty(result))
                return LevelsPrefsData.Empty();

            return JsonUtility.FromJson<LevelsPrefsData>(result);
        }

        public bool IsLevelOpened(int id)
        {
            return !string.IsNullOrEmpty(
                PlayerPrefs.GetString(_levelsConfig.GetLevelByIndex(id).levelDataConfig.levelUniqueId));
        }

        public void WriteResult(int levelId, TimeSpan time)
        {
            if (IsLevelOpened(levelId))
            {
                var result =
                    PlayerPrefs.GetString(_levelsConfig.GetLevelByIndex(levelId).levelDataConfig.levelUniqueId);
                var data = JsonUtility.FromJson<LevelsPrefsData>(result);
                
                if (data.timeFly <= time.TotalSeconds)
                    return;
            }

            PlayerPrefs.SetString(_levelsConfig.GetLevelByIndex(levelId).levelDataConfig.levelUniqueId,
                JsonUtility.ToJson(new LevelsPrefsData(time, Application.version)));
        }

        private void Init()
        {
            _levelsConfig.Init();
            
            if (PlayerPrefs.HasKey(ObsoletePrefsName))
                UpdateSaves();

            for (var i = 0; i < _levelsConfig.CountGameLevels; i++)
            {
                if (!IsLevelOpened(i))
                {
                    LastOpenedLevel = i;
                    break;
                }
            }
        }

        private void UpdateSaves()
        {
            var lastOpenedLevel = PlayerPrefs.GetInt(ObsoletePrefsName);

            for (var i = 0; i < lastOpenedLevel && i < _levelsConfig.CountGameLevels; i++)
            {
                PlayerPrefs.SetString(_levelsConfig.GetLevelByIndex(i).levelDataConfig.levelUniqueId, 
                    JsonUtility.ToJson(LevelsPrefsData.Empty()));
            }

            PlayerPrefs.DeleteKey(ObsoletePrefsName);
        }
    }
}