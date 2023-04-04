using System;
using UnityEngine.AddressableAssets;

namespace Addressables.Levels
{
    [Serializable]
    public class LevelGroup
    {
        public string groupName;
        public AssetReference[] levelsReferences;
    }
}