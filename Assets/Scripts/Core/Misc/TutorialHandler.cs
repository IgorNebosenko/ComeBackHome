using UnityEngine;

namespace CBH.Core.Misc
{
    public class TutorialHandler
    {
        private const string NeedTutorialPrefsName = "NeedTutorial";

        public bool IsNeedShowTutorial()
        {
            return PlayerPrefs.GetInt(NeedTutorialPrefsName, 0) == 0;
        }

        public void SetCompletedState()
        {
            PlayerPrefs.SetInt(NeedTutorialPrefsName, 1);
        }
    }
}