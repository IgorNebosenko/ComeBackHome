using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core.IAP;
using UnityEngine;

namespace CBH.Ads
{
    public class AndroidAdsProvider : IAdsProvider
    {
        private IAnalyticsManager _analyticsManager;
        private IStorePurchaseController _storePurchaseController;
        private AdsConfig _adsConfig;
        private AdsData _adsData;
        
        private const string AdsKey = "18895eccd";

        public AndroidAdsProvider(IAnalyticsManager analyticsManager, IStorePurchaseController _storePurchaseController,
            AdsConfig adsConfig, AdsData adsData)
        {
            _analyticsManager = analyticsManager;
            _adsConfig = adsConfig;
            _adsData = adsData;
            
            IronSourceConfig.Instance.setClientSideCallbacks(true);
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(AdsKey);
            
            LoadInterstitial();
        }
        
        public void LoadInterstitial()
        {
            IronSource.Agent.loadInterstitial();
        }

        public void ShowInterstitial()
        {
            _analyticsManager.SendEvent(new ShowInterstitialAdEvent(
                _adsData.timeFlyFromLastAd >= _adsConfig.timeFlyBetweenAds,
                _storePurchaseController.HasNoAdsSubscription,
                IronSource.Agent.isInterstitialReady().ToString()));
            
            if (IronSource.Agent.isInterstitialReady())
                IronSource.Agent.showInterstitial();
            
            LoadInterstitial();
        }
    }
}