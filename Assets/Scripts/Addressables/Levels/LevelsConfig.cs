using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Addressables.Levels
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private AssetReference mainMenuScene;
        [SerializeField] private AssetReference tutorialLevel;
        [Space] 
        [SerializeField] private LevelGroup[] levelsGroups;
        [Space]
        [SerializeField] private AssetReference finalLevel;
        
        
    }
}