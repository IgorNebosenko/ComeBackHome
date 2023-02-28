using System.Collections;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UniRx;
using UnityEngine.SceneManagement;

namespace CBH.UI.Menu.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private IAnalyticsManager _analyticsManager;
        private GameData _gameData;
        private ViewManager _viewManager;
        private PopupManager _popupManager;

        public int CurrentLevel => _gameData.LastCompletedScene;
        public int MaxGameLevel => GameData.CountLevels;
        
        public MainMenuPresenter(IAnalyticsManager analyticsManager, GameData gameData, PopupManager popupManager,
            ViewManager viewManager, MainMenuView view) : base(view)
        {
            _analyticsManager = analyticsManager;
            _gameData = gameData;
            _viewManager = viewManager;
            _popupManager = popupManager;

            view.SubscribeEvents(this);
        }

        public void OnPlayButtonPressed()
        {
            Observable.FromCoroutine(LoadSceneProcess).Subscribe();
        }

        public void OnLevelSelectButtonPressed()
        {
            _viewManager.ShowView<LevelSelectPresenter>();
        }

        public void OnHowPlayButtonPressed()
        {
            _viewManager.ShowView<HowPlayPresenter>();
        }
        
        public void OnSettingsButtonPressed()
        {
            _viewManager.ShowView<SettingsPresenter>();
        }

        private IEnumerator LoadSceneProcess()
        {
            yield return SceneManager.LoadSceneAsync(_gameData.LastCompletedScene);
        }

        public void OnNoAdsButtonPressed()
        {
            _analyticsManager.SendEvent(new OpenBuyNoAdsEvent(_gameData.LastCompletedScene));
            _popupManager.ShowPopup<NoAdsSubscriptionPresenter>();
        }
    }
}