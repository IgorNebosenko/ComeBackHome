namespace CBH.Analytics.Events
{
    public class LoadNextGameLevelEvent : AnalyticsEvent
    {
        public override string Key => "load_next_game_level";

        public LoadNextGameLevelEvent(int levelId, string levelName, int maxOpenedLevel, float totalTimeBeforeAd, int totalRestartsBeforeAd)
        {
            Data.Add("level_id", levelId);
            Data.Add("level_name", levelName);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("total_time_before_ad", totalTimeBeforeAd);
            Data.Add("total_restarts_before_ad", totalRestartsBeforeAd);
        }
    }
}