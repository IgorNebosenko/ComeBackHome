using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CBH.Core.IAP
{
    public class GooglePlayStoreModule : IStoreListener, IStorePurchaseController
    {
        private IStoreController _storeController;
        private bool _hasNoAds;

        private const string SubscriptionId = "no_ads_mounth";

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

        public GooglePlayStoreModule()
        {
            var configBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            configBuilder.Configure<IGooglePlayConfiguration>().SetServiceDisconnectAtInitializeListener(() =>
            {
                Debug.LogWarning($"[{GetType().Name}] Can't connect to google play services!");
            });
            configBuilder.AddProduct(SubscriptionId, ProductType.Subscription);
            UnityPurchasing.Initialize(this, configBuilder);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogWarning($"[{GetType().Name}] error of init! Reason: {error}");
        }
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;

            ResolveProducts();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogWarning($"[{GetType().Name}] purchase failed! Reason: {failureReason}");
        }

        private void ResolveProducts()
        {
            if (_storeController == null)
                return;

            var product = _storeController.products.WithID(SubscriptionId);
            if (product == null || !product.availableToPurchase)
                return;

            var sm = new SubscriptionManager(product, null);
            HasNoAdsSubscription = sm.getSubscriptionInfo().isSubscribed() == Result.True;
        }

        public bool TryPurchaseSubscription()
        {
            if (_storeController == null)
                return false;

            var product = _storeController.products.WithID(SubscriptionId);
            if (product == null || !product.availableToPurchase)
                return false;
            
            _storeController.InitiatePurchase(product);
            var sm = new SubscriptionManager(product, null);
            HasNoAdsSubscription = sm.getSubscriptionInfo().isSubscribed() == Result.True;
            return HasNoAdsSubscription;
        }

        public string GetNoAdsSubscriptionCost()
        {
            if (_storeController == null)
                return "Not available";

            var product = _storeController.products.WithID(SubscriptionId);
            if (product == null || !product.availableToPurchase)
                return "Not available";

            return $"{product.metadata.localizedPrice} {product.metadata.isoCurrencyCode}";
        }
    }
}