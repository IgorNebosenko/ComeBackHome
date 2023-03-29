using UnityEngine;

namespace CBH.Core.Core.Misc
{
    public class GlobalUserSettings
    {
        private const string IsLeftPositionBoostButtonKey = "IsLeftPositionBoostButton";

        public bool IsLeftPositionBoost
        {
            get => PlayerPrefs.GetInt(IsLeftPositionBoostButtonKey, 0) != 0;
            set => PlayerPrefs.SetInt(IsLeftPositionBoostButtonKey, value ? 1 : 0);
        }
    }
}