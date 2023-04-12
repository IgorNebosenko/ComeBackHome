using System.Collections;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.IAP;
using CBH.Core.Levels;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.UI.Menu.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private IAnalyticsManager _analyticsManager;
        private ILevelsManager _levelsManager;
        private IUserLevelsInfo _userLevelsInfo;
        private ViewManager _viewManager;
        private PopupManager _popupManager;

        public int CurrentLevel => _userLevelsInfo.LastOpenedLevel;
        public int MaxGameLevel => _userLevelsInfo.TotalLevels;
        
        public MainMenuPresenter(MainMenuView view, IAnalyticsManager analyticsManager, ILevelsManager levelsManager, 
            IUserLevelsInfo userLevelsInfo, PopupManager popupManager,
            ViewManager viewManager, IStorePurchaseController storePurchaseController) : base(view)
        {
            _analyticsManager = analyticsManager;
            _levelsManager = levelsManager;
            _userLevelsInfo = userLevelsInfo;
            
            _viewManager = viewManager;
            _popupManager = popupManager;
        }

        public void OnPlayButtonPressed()
        {
            Observable.FromCoroutine(LoadSceneProcess).Subscribe();
        }

        public void OnLevelSelectButtonPressed()
        {
            _analyticsManager.SendEvent(new OpenLevelSelectMenuEvent(_userLevelsInfo.LastOpenedLevel));
            _viewManager.ShowView<LevelSelectPresenter>();
        }

        public void OnHowPlayButtonPressed()
        {
            _analyticsManager.SendEvent(new HowToPlayMenuEvent());
            _viewManager.ShowView<HowPlayPresenter>();
        }
        
        public void OnSettingsButtonPressed()
        {
            _analyticsManager.SendEvent(new SettingsOpenMenuEvent());
            _viewManager.ShowView<SettingsPresenter>();
        }

        private IEnumerator LoadSceneProcess()
        {
            _analyticsManager.SendEvent(new ContinueLevelFromMenuEvent(_userLevelsInfo.LastOpenedLevel));
            yield return _levelsManager.ContinueGame();
        }

        public void OnNoAdsButtonPressed()
        {
            _analyticsManager.SendEvent(new OpenBuyNoAdsEvent(_userLevelsInfo.LastOpenedLevel));
            _popupManager.ShowPopup<NoAdsSubscriptionPresenter>();
        }
    }
}