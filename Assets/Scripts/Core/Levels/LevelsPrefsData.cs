using System;
using UnityEngine;

namespace CBH.Core.Levels
{
    [Serializable]
    public class LevelsPrefsData
    {
        public TimeSpan timeFly;
        public string gameVersion;

        public LevelsPrefsData(TimeSpan timeFly, string gameVersion)
        {
            this.timeFly = timeFly;
            this.gameVersion = gameVersion;
        }

        public static LevelsPrefsData Empty()
        {
            return new LevelsPrefsData(TimeSpan.MinValue, Application.version);
        }
    }
}