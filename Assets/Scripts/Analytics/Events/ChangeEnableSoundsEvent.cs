namespace CBH.Analytics.Events
{
    public class ChangeEnableSoundsEvent : AnalyticsEvent
    {
        public override string Key => "change_enable_sounds";

        public ChangeEnableSoundsEvent(bool state)
        {
            Data.Add("state", state);
        }
    }
}