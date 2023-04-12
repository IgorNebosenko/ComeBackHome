namespace CBH.Analytics.Events
{
    public class LandingOnLandingPadEvent : AnalyticsEvent
    {
        public override string Key => "landing_on_landing_pad";

        public LandingOnLandingPadEvent(int levelId, string levelName, int maxOpenedLevel, float flyTime, int attemptLand)
        {
            Data.Add("level_id", levelId);
            Data.Add("level_name", levelName);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("fly_time", flyTime);
            Data.Add("attempt_land", attemptLand);
        }
    }
}