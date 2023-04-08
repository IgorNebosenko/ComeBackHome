using System;
using UnityEngine;

namespace CBH.Core.Configs.Levels
{
    [Serializable]
    public class LevelGroupConfig
    {
        [SerializeField] private string groupName;
        public int buildIndex;
        public LevelDataConfig[] levelsConfig;
    }
}