namespace CBH.Analytics.Events
{
    public class OpenLeaderboardEvent : AnalyticsEvent
    {
        public override string Key => "open_leaderboard";

        public OpenLeaderboardEvent(int selectedLevel, int maxOpenedLevel, float bestLevelTime = -1)
        {
            Data.Add("selected_level", selectedLevel);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("best_level_time", bestLevelTime);
        }
    }
}