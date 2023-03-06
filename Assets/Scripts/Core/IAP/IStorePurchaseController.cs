using System;

namespace CBH.Core.IAP
{
    public interface IStorePurchaseController
    {
        bool HasNoAdsSubscription { get; }
        event Action<bool> SubscriptionStatusUpdated;

        bool TryPurchaseSubscription();
        string GetNoAdsSubscriptionCost();
    }
}