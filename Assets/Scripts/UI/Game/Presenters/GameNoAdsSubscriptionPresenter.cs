using System;
using System.Collections.Generic;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.IAP;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.UI.Game.Presenters
{
    public class GameNoAdsPopupArgs : PopupArgs
    {
        public float chanceToBeShown;
    }

    public class GameNoAdsSubscriptionPresenter : PopupPresenterCoroutine<GameNoAdsSubscriptionPopup, GameNoAdsPopupArgs, PopupResult>
    {
        private IAnalyticsManager _analyticsManager;
        private IStorePurchaseController _storePurchaseController;
        private GameData _gameData;

        private float _startShowingTime;
        private GameNoAdsPopupArgs _args;

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

        public override IEnumerable<PopupResult> Init(GameNoAdsPopupArgs args)
        {
            _args = args;
            yield return null;
        }

        public void OnButtonBuyPressed()
        {
            var result = _storePurchaseController.TryPurchaseSubscription();
            _analyticsManager.SendEvent(new GameResultBuyNoAdsEvent(SceneManager.GetActiveScene().buildIndex, 
                _args.chanceToBeShown, _gameData.LastCompletedScene, result));
            SubscriptionStatusChanged?.Invoke(result);
        }

        public void OnButtonClosePressed()
        {
            var timeShowingPopup = Time.realtimeSinceStartup - _startShowingTime;

            _analyticsManager.SendEvent(new GameNoAdsShowResultEvent(SceneManager.GetActiveScene().buildIndex, 
                _args.chanceToBeShown, _gameData.LastCompletedScene, 
                !_storePurchaseController.HasNoAdsSubscription, timeShowingPopup));

            Close();
        }

        public void SendEventShowPopup()
        {
            _analyticsManager.SendEvent(new ShowGameNoAdsPopupEvent(SceneManager.GetActiveScene().buildIndex, 
                _args.chanceToBeShown, _gameData.LastCompletedScene));
        }
    }
}