namespace CBH.Analytics.Events
{
    public class NoAdsShowResultEvent : AnalyticsEvent
    {
        public override string Key => "no_ads_show_result";

        public NoAdsShowResultEvent(int maxOpenedLevel, bool isClosedWithoutBuy, float timeBeforeClose = -1)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("is_closed_without_buy", isClosedWithoutBuy);
            Data.Add("time_before_close", timeBeforeClose);
        }
    }
}