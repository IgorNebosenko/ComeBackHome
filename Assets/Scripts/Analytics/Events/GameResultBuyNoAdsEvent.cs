namespace CBH.Analytics.Events
{
    public class GameResultBuyNoAdsEvent : AnalyticsEvent
    {
        public override string Key => "game_result_buy_no_ads";

        public GameResultBuyNoAdsEvent(int currentLevel, float chanceToBeShown, int maxOpenedLevel,
            bool noAdsBuyResult)
        {
            Data.Add("current_level", currentLevel);
            Data.Add("chance_to_be_shown", chanceToBeShown);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("no_ads_buy_result", noAdsBuyResult);
        }
    }
}