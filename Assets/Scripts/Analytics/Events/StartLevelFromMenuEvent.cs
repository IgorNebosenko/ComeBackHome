namespace CBH.Analytics.Events
{
    public class StartLevelFromMenuEvent : AnalyticsEvent
    {
        public override string Key => "start_level_from_menu";

        public StartLevelFromMenuEvent(int levelId, int maxOpenedLevel)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_leve;", maxOpenedLevel);
        }
    }
}