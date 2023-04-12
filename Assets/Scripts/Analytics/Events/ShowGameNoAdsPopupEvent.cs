﻿namespace CBH.Analytics.Events
{
    public class ShowGameNoAdsPopupEvent : AnalyticsEvent
    {
        public override string Key => "show_game_no_ads_popup";

        public ShowGameNoAdsPopupEvent(int currentLevel, string currentLevelName, float chanceToBeShown, int maxOpenedLevel)
        {
            Data.Add("current_level", currentLevel);
            Data.Add("current_level_name", currentLevelName);
            Data.Add("chance_to_be_shown", chanceToBeShown);
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}