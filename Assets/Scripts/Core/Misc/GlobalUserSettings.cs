using UnityEngine;

namespace CBH.Core.Core.Misc
{
    public class GlobalUserSettings
    {
        private const string IsRightPositionBoostButtonKey = "IsRightPositionBoostButton";

        public bool IsRightPositionBoost
        {
            get => PlayerPrefs.GetInt(IsRightPositionBoostButtonKey, 0) != 0;
            set => PlayerPrefs.SetInt(IsRightPositionBoostButtonKey, value ? 1 : 0);
        }
    }
}