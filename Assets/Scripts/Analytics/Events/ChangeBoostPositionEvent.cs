namespace CBH.Analytics.Events
{
    public class ChangeBoostPositionEvent : AnalyticsEvent
    {
        public override string Key => "change_boost_button_position";

        public ChangeBoostPositionEvent(bool isRightPosition)
        {
            Data.Add("is_right_position", isRightPosition);
        }
    }
}