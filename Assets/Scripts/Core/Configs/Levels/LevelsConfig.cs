using UnityEngine;

namespace CBH.Core.Configs.Levels
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        public LevelGroupConfig tutorialGroup;

        [SerializeField] private LevelGroupConfig[] levelGroupConfigs;
    }
}