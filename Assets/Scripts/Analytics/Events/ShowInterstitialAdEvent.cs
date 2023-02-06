namespace CBH.Analytics.Events
{
    public class ShowInterstitialAdEvent : AnalyticsEvent
    {
        public override string Key => "show_interstitial_ad";

        public ShowInterstitialAdEvent(bool showByPlayTime, bool hasNoAds, string adsShowResult)
        {
            Data.Add("show_by_play_time", showByPlayTime);
            Data.Add("has_no_ads", hasNoAds);
            Data.Add("ads_show_result", adsShowResult);
        }
    }
}