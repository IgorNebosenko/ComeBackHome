namespace CBH.Analytics.Events
{
    public class LaunchFromLaunchPadEvent : AnalyticsEvent
    {
        public override string Key => "launch_from_launch_pad";

        public LaunchFromLaunchPadEvent(int levelId, int maxOpenedLevel, float timeFromLoad)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("time_from_load", timeFromLoad);
        }
    }
}