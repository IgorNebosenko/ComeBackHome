using System;
using System.Collections.Generic;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.IAP;
using CBH.Core.Levels;
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
        private ILevelsManager _levelsManager;
        private IUserLevelsInfo _userLevelsInfo;

        private float _startShowingTime;
        private GameNoAdsPopupArgs _args;
        private GameManager _gameManager;

        public event Action<bool> SubscriptionStatusChanged;

        public string CostLocalized => _storePurchaseController.GetNoAdsSubscriptionCost();
        public bool HasNoAds => _storePurchaseController.HasNoAdsSubscription;

        public GameNoAdsSubscriptionPresenter(GameNoAdsSubscriptionPopup view, GameManager gameManager,
            IAnalyticsManager analyticsManager, IStorePurchaseController storePurchaseController,
            ILevelsManager levelsManager, IUserLevelsInfo userLevelsInfo) : base(view)
        {
            _analyticsManager = analyticsManager;
            _storePurchaseController = storePurchaseController;
            _gameManager = gameManager;
            _levelsManager = levelsManager;
            _userLevelsInfo = userLevelsInfo;

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
            
            _analyticsManager.SendEvent(new ShowGameNoAdsPopupEvent(_levelsManager.CurrentLevelId, 
                _levelsManager.CurrentLevel.levelDataConfig.levelUniqueId, _args.chanceToBeShown,
                _userLevelsInfo.LastOpenedLevel));
        }

        public void OnButtonBuyPressed()
        {
            var result = _storePurchaseController.TryPurchaseSubscription();
            _analyticsManager.SendEvent(new GameResultBuyNoAdsEvent(_levelsManager.CurrentLevelId, 
                _levelsManager.CurrentLevel.levelDataConfig.levelUniqueId, _args.chanceToBeShown,
                _userLevelsInfo.LastOpenedLevel, result));
            SubscriptionStatusChanged?.Invoke(result);
        }

        public void OnButtonClosePressed()
        {
            var timeShowingPopup = Time.realtimeSinceStartup - _startShowingTime;

            _analyticsManager.SendEvent(new GameNoAdsShowResultEvent(_levelsManager.CurrentLevelId, 
                _levelsManager.CurrentLevel.levelDataConfig.levelUniqueId, _args.chanceToBeShown,
                _userLevelsInfo.LastOpenedLevel, !_storePurchaseController.HasNoAdsSubscription, timeShowingPopup));

            Close();
        }
    }
}