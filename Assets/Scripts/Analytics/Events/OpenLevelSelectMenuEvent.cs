namespace CBH.Analytics.Events
{
    public class OpenLevelSelectMenuEvent : AnalyticsEvent
    {
        public override string Key => "open_level_select_menu_event";

        public OpenLevelSelectMenuEvent(int maxOpenedLevel)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}