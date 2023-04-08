using System;
using CBH.Core.Audio;
using UnityEngine;

namespace CBH.Core.Configs.Levels
{
    [Serializable]
    public class LevelDataConfig
    {
        public string levelUniqueId;
        public GameObject visual;
        public Vector3 launchPadPosition;
        public Vector3 landingPadPosition;
        public Vector3 rocketPosition;
        [Space]
        public MusicClip musicClip = MusicClip.MainTheme;
        public Vector3 gameGravity = Vector3.down * 40;
    }
}