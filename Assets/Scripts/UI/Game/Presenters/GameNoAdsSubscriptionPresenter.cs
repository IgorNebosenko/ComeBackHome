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

        public GameNoAdsPopupArgs(float chanceToBeShown)
        {
            this.chanceToBeShown = chanceToBeShown;
        }
    }

    public class GameNoAdsSubscriptionPresenter : PopupPresenterCoroutine<GameNoAdsSubscriptionPopup, GameNoAdsPopupArgs, PopupResult>
    {
        private IAnalyticsManager _analyticsManager;
        private IStorePurchaseController _storePurchaseController;
        private GameData _gameData;

        private float _startShowingTime;
        private GameNoAdsPopupArgs _args;
        private GameManager _gameManager;

        public event Action<bool> SubscriptionStatusChanged;

        public string CostLocalized => _storePurchaseController.GetNoAdsSubscriptionCost();
        public bool HasNoAds => _storePurchaseController.HasNoAdsSubscription;

        public GameNoAdsSubscriptionPresenter(GameNoAdsSubscriptionPopup view, GameManager gameManager,
            IAnalyticsManager analyticsManager, IStorePurchaseController storePurchaseController,
            GameData gameData) : base(view)
        {
            _analyticsManager = analyticsManager;
            _storePurchaseController = storePurchaseController;
            _gameData = gameData;
            _gameManager = gameManager;

            _startShowingTime = Time.realtimeSinceStartup;
        }

        public override IEnumerable<PopupResult> Init(GameNoAdsPopupArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void Closing()
        {
            _gameManager.IsInterruptedForPopup = false;
        }

        public void SetArgs(GameNoAdsPopupArgs args)
        {
            _args = args;
            
            _analyticsManager.SendEvent(new ShowGameNoAdsPopupEvent(SceneManager.GetActiveScene().buildIndex, 
                _args.chanceToBeShown, _gameData.LastCompletedScene));
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
    }
}