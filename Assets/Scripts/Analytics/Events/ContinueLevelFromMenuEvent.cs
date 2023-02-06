namespace CBH.Analytics.Events
{
    public class ContinueLevelFromMenuEvent : AnalyticsEvent
    {
        public override string Key => "continue_level_from_menu";

        public ContinueLevelFromMenuEvent(int maxOpenedLevel)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}