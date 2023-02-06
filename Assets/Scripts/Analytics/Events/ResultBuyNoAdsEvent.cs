namespace CBH.Analytics.Events
{
    public class ResultBuyNoAdsEvent : AnalyticsEvent
    {
        public override string Key => "result_buy_no_ads";

        public ResultBuyNoAdsEvent(int maxOpenedLevel, string noAdsBuyResult)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("no_ads_buy_result", noAdsBuyResult);
        }
    }
}