namespace CBH.Analytics.Events
{
    public class RestartLevelEvent : AnalyticsEvent
    {
        public override string Key => "restart_level";

        public RestartLevelEvent(int levelId, string levelName, int maxOpenedLevel, float totalTimeBeforeAd, int totalRestartsBeforeAd,
            bool needShowAd, bool playerHasNoAds)
        {
            Data.Add("level_id", levelId);
            Data.Add("level_name", levelName);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("total_time_before_ad", totalTimeBeforeAd);
            Data.Add("total_restart_before_ad", totalRestartsBeforeAd);
            Data.Add("need_show_ad", needShowAd);
            Data.Add("player_has_no_ads", playerHasNoAds);
        }
    }
}