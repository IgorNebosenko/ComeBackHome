    using System.Collections.Generic;
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

            private List<AssetReference> _listLevels;

            public AssetReference MainMenu => mainMenuScene;
            public AssetReference TutorialLevel => tutorialLevel;

            public int CountLevels => _listLevels.Count;

            public void Init()
            {
                _listLevels = new List<AssetReference>();
                foreach (var levelsGroup in levelsGroups)
                    _listLevels.AddRange(levelsGroup.levelsReferences);
                
                for (var i = 0; i < _listLevels.Count; i++)
                    Debug.Log(GetLevelUniqueId(i));
            }

            public AssetReference GetReferenceById(int id)
            {
                return id >= _listLevels.Count ? finalLevel : _listLevels[id];
            }

            public string GetLevelUniqueId(int id)
            {
                return id >= _listLevels.Count ? finalLevel.AssetGUID : _listLevels[id].AssetGUID;
            }
        }
    }