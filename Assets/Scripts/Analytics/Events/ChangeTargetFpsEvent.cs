namespace CBH.Analytics.Events
{
    public class ChangeTargetFpsEvent : AnalyticsEvent
    {
        public override string Key => "change_target_fps";

        public ChangeTargetFpsEvent(int fps)
        {
            Data.Add("fps", fps);
        }
    }
}