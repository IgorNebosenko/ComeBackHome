using System;

#if UNITY_EDITOR
namespace CBH.Core.IAP
{
    public class EditorStoreModule : IStorePurchaseController
    {
        private bool _hasNoAds;

        public bool HasNoAdsSubscription
        {
            get => _hasNoAds;
            set
            {
                _hasNoAds = value;
                SubscriptionStatusUpdated?.Invoke(_hasNoAds);
            }
        }
        public event Action<bool> SubscriptionStatusUpdated;

        public bool TryPurchaseSubscription()
        {
            HasNoAdsSubscription = true;
            return HasNoAdsSubscription;
        }

        public string GetNoAdsSubscriptionCost()
        {
            return "Free";
        }
    }
}
#endif