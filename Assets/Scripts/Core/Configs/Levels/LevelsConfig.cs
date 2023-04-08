using System.Collections.Generic;
using UnityEngine;

namespace CBH.Core.Configs.Levels
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        public LevelGroupConfig tutorialGroup;
        [SerializeField] private LevelGroupConfig[] levelGroupConfigs;
        public int lastLevelIndex;

        private List<LevelDataPair> _levelDataPairs;

        public int CountGameLevels => _levelDataPairs.Count;

        public LevelsConfig()
        {
            _levelDataPairs = new List<LevelDataPair>();
        }

        public void Init()
        {
            foreach (var levelGroupConfig in levelGroupConfigs)
            {
                foreach (var levelData in levelGroupConfig.levelsConfig)
                {
                    _levelDataPairs.Add(new LevelDataPair(levelGroupConfig.buildIndex, levelData));
                }
            }
        }

        public LevelDataPair GetLevelByIndex(int index)
        {
            if (index < 0 || index >= _levelDataPairs.Count)
                return null;

            return _levelDataPairs[index];
        }
    }
}