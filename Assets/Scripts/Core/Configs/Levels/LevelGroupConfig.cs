using UnityEngine;

namespace CBH.Core.Configs.Levels
{
    public class LevelGroupConfig
    {
        [SerializeField] private string groupName;
        public int buildIndex;
        public LevelDataConfig[] levelsConfig;
    }
}