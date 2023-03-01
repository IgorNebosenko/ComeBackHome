namespace CBH.Analytics.Events
{
    public class CloseLevelSelectMenuEvent : AnalyticsEvent
    {
        public override string Key => "close_level_select_menu";

        public CloseLevelSelectMenuEvent(int maxOpenedLevel)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}