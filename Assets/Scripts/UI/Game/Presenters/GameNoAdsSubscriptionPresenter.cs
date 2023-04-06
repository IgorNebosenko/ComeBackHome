﻿using System;
using System.Collections.Generic;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.IAP;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using UnityEngine;

namespace CBH.UI.Game.Presenters
{
    public class GameNoAdsSubscriptionPresenter : PopupPresenterCoroutine<GameNoAdsSubscriptionPopup, PopupArgs, PopupResult>
    {
        private IAnalyticsManager _analyticsManager;
        private IStorePurchaseController _storePurchaseController;
        private GameData _gameData;

        private float _startShowingTime;

        public event Action<bool> SubscriptionStatusChanged;

        public string CostLocalized => _storePurchaseController.GetNoAdsSubscriptionCost();
        public bool HasNoAds => _storePurchaseController.HasNoAdsSubscription;

        public GameNoAdsSubscriptionPresenter(GameNoAdsSubscriptionPopup view, IAnalyticsManager analyticsManager,
            IStorePurchaseController storePurchaseController,
            GameData gameData) : base(view)
        {
            _analyticsManager = analyticsManager;
            _storePurchaseController = storePurchaseController;
            _gameData = gameData;

            _startShowingTime = Time.realtimeSinceStartup;
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void OnButtonBuyPressed()
        {
            var result = _storePurchaseController.TryPurchaseSubscription();
            _analyticsManager.SendEvent(new ResultBuyNoAdsEvent(_gameData.LastCompletedScene, result));
            SubscriptionStatusChanged?.Invoke(result);
        }

        public void OnButtonClosePressed()
        {
            var timeShowingPopup = Time.realtimeSinceStartup - _startShowingTime;

            _analyticsManager.SendEvent(new NoAdsShowResultEvent(_gameData.LastCompletedScene,
                !_storePurchaseController.HasNoAdsSubscription, timeShowingPopup));

            Close();
        }

        public void SendEventShowPopup()
        {
            _analyticsManager.SendEvent(new ShowNoAdsPopupEvent(_gameData.LastCompletedScene));
        }
    }
}