using System;
using System.Collections.Generic;
using CBH.Ads;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.IAP;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using UnityEngine;

namespace CBH.UI.Menu.Presenters
{
    public class NoAdsSubscriptionPresenter : PopupPresenterCoroutine<NoAdsSubscriptionPopup, PopupArgs, PopupResult>
    {
        //private IAnalyticsManager _analyticsManager;
        private IStorePurchaseController _storePurchaseController;
        private GameData _gameData;
        private AdsData _adsData;

        private float _startShowingTime;

        public event Action<bool> SubscriptionStatusChanged;

        public string CostLocalized => _storePurchaseController.GetNoAdsSubscriptionCost();
        public bool HasNoAds => _storePurchaseController.HasNoAdsSubscription;
        
        public NoAdsSubscriptionPresenter(NoAdsSubscriptionPopup view, IAnalyticsManager analyticsManager, IStorePurchaseController storePurchaseController,
            GameData gameData, AdsData adsData) : base(view)
        {
            //_analyticsManager = analyticsManager;
            _storePurchaseController = storePurchaseController;
            _gameData = gameData;
            _adsData = adsData;

            _startShowingTime = Time.realtimeSinceStartup;
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void OnButtonBuyPressed()
        {
            var result = _storePurchaseController.TryPurchaseSubscription();
            //_analyticsManager.SendEvent(new ResultBuyNoAdsEvent(_gameData.LastCompletedScene, result));
            SubscriptionStatusChanged?.Invoke(result);
        }

        public void OnButtonClosePressed()
        {
            var timeShowingPopup = Time.realtimeSinceStartup - _startShowingTime;

            //_analyticsManager.SendEvent(new NoAdsShowResultEvent(_gameData.LastCompletedScene, !_storePurchaseController.HasNoAdsSubscription, timeShowingPopup));
            
            Close();
        }

        public void SendEventShowPopup()
        {
            //_analyticsManager.SendEvent(new ShowNoAdsPopupEvent(_gameData.LastCompletedScene));
        }
    }
}