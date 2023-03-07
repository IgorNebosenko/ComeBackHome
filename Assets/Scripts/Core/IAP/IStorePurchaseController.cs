namespace CBH.Core.IAP
{
    public interface IStorePurchaseController
    {
        bool HasNoAdsSubscription { get; }

        bool TryPurchaseSubscription();
        string GetNoAdsSubscriptionCost();
    }
}