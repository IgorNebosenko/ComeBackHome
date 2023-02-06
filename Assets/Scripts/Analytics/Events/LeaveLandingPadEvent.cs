namespace CBH.Analytics.Events
{
    public class LeaveLandingPadEvent : AnalyticsEvent
    {
        public override string Key => "leave_landing_pad";

        public LeaveLandingPadEvent(int levelId, int maxOpenedLevel, float flyTime, int attemptLand, float timeLeft)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("fly_time", flyTime);
            Data.Add("attempt_land", attemptLand);
            Data.Add("time_left", timeLeft);
        }
    }
}