namespace CBH.Analytics.Events
{
    public class ToMenuButtonPressedEvent : AnalyticsEvent
    {
        public override string Key => "to_menu_button_pressed";

        public ToMenuButtonPressedEvent(int levelId, int maxOpenedLevel, float flyTime)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("fly_time", flyTime);
        }
    }
}