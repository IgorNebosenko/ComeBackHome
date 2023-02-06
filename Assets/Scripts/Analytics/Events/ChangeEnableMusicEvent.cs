namespace CBH.Analytics.Events
{
    public class ChangeEnableMusicEvent : AnalyticsEvent
    {
        public override string Key => "change_enable_music";

        public ChangeEnableMusicEvent(bool state)
        {
            Data.Add("state", state);
        }
    }
}