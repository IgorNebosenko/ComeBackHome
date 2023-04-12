namespace CBH.Analytics.Events
{
    public class GameNoAdsShowResultEvent : AnalyticsEvent
    {
        public override string Key => "game_no_ads_show_result";

        public GameNoAdsShowResultEvent(int currentLevel, string currentLevelName, float chanceToBeShown, int maxOpenedLevel, 
            bool isClosedWithoutBuy, float timeBeforeClose)
        {
            Data.Add("current_level", currentLevel);
            Data.Add("current_level_name", currentLevelName);
            Data.Add("chance_to_be_shown", chanceToBeShown);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("is_closed_without_buy", isClosedWithoutBuy);
            Data.Add("time_before_close", timeBeforeClose);
        }
    }
}