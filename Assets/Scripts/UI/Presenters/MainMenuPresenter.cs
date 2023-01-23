using System.Collections;
using CBH.Core;
using CBH.UI.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CBH.UI.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private GameData _gameData;
        private ViewManager _viewManager;
        private PopupManager _popupManager;

        public int CurrentLevel => _gameData.CurrentScene;
        public int MaxGameLevel => GameData.CountLevels;
        
        public MainMenuPresenter(MainMenuView view, GameData gameData, ViewManager viewManager, PopupManager popupManager) :
            base(view)
        {
            _gameData = gameData;
            _viewManager = viewManager;
            _popupManager = popupManager;
            
            view.SubscribeEvents(this);
        }

        public void OnNewButtonPressed()
        {
            if (_gameData.CurrentScene == 1)
                Observable.FromCoroutine(LoadSceneProcess).Subscribe();
            else
                Debug.Log("Show popup about reset game");
        }
        
        public void OnContinueButtonPressed()
        {
            Observable.FromCoroutine(LoadSceneProcess).Subscribe();
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
            yield return SceneManager.LoadSceneAsync(_gameData.CurrentScene);
            Close();
        }
    }
}