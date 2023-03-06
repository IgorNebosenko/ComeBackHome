//#if UNITY_EDITOR
namespace CBH.Core.IAP
{
    public class EditorStoreModule : IStorePurchaseController
    {
        public bool HasNoAdsSubscription { get; private set; }
        
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
//#endif