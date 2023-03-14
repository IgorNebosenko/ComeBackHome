using System.Collections;
using UniRx;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CBH.Core.IAP
{
    public class GooglePlayStoreModule : IStoreListener, IStorePurchaseController
    {
        private IStoreController _storeController;

        private const string SubscriptionId = "no_ads_mounth";

        public bool HasNoAdsSubscription { get; private set; }

        public GooglePlayStoreModule()
        {
            Observable.FromCoroutine(InitProcess).Subscribe();
        }
        
        private IEnumerator InitProcess()
        {
            yield return UnityServices.InitializeAsync();
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

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogWarning($"[{GetType().Name}] error of init! Reason: {error}, message: {message}");
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

            if (product.hasReceipt)
            {
                var sm = new SubscriptionManager(product, product.definition.storeSpecificId);

                HasNoAdsSubscription = sm.getSubscriptionInfo().isFreeTrial() == Result.True ||
                                       sm.getSubscriptionInfo().isSubscribed() == Result.True ||
                                       sm.getSubscriptionInfo().isAutoRenewing() == Result.True;
            }
            else
                HasNoAdsSubscription = false;
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