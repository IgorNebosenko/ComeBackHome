using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/Level config")]
    public class LevelConfigData : ScriptableObject
    {
        public LevelConfig[] levels;
    }
}