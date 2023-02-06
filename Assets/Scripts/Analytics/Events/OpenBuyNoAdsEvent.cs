namespace CBH.Analytics.Events
{
    public class OpenBuyNoAdsEvent : AnalyticsEvent
    {
        public override string Key => "open_buy_no_ads";

        public OpenBuyNoAdsEvent(int maxOpenedLevel)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}