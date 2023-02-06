using System.Collections;
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
        private GameData _gameData;
        private ViewManager _viewManager;

        public int CurrentLevel => _gameData.LastCompletedScene;
        public int MaxGameLevel => GameData.CountLevels;
        
        public MainMenuPresenter(MainMenuView view, GameData gameData, ViewManager viewManager) : base(view)
        {
            _gameData = gameData;
            _viewManager = viewManager;

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
    }
}