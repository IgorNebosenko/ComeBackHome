namespace CBH.Analytics.Events
{
    public class ShowNoAdsPopupEvent : AnalyticsEvent
    {
        public override string Key => "show_no_ads_popup";

        public ShowNoAdsPopupEvent(int maxOpenedLevel)
        {
            Data.Add("max_opened_level", maxOpenedLevel);
        }
    }
}