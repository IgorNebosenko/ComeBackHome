using System;
using UnityEngine;

namespace CBH.Core.Configs
{
    [CreateAssetMenu(fileName = "FpsConfig", menuName = "Configs/FPS")]
    public class FpsConfig : ScriptableObject
    {
        public FpsConfigData[] config;
    }

    [Serializable]
    public class FpsConfigData
    {
        public string name;
        public int fps;
    }
}