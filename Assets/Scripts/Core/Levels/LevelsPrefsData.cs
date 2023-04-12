using System;
using UnityEngine;

namespace CBH.Core.Levels
{
    [Serializable]
    public class LevelsPrefsData
    {
        public double timeFly;
        public string gameVersion;

        public LevelsPrefsData(TimeSpan timeFly, string gameVersion)
        {
            this.timeFly = timeFly.TotalSeconds;
            this.gameVersion = gameVersion;
        }

        public static LevelsPrefsData Empty()
        {
            return new LevelsPrefsData(TimeSpan.MinValue, Application.version);
        }
    }
}