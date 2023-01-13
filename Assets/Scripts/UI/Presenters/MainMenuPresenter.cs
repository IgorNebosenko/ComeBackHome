using CBH.Core;
using CBH.UI.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UnityEngine;

namespace CBH.UI.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private GameData _gameData;
        private ViewManager _viewManager;

        public int CurrentLevel => _gameData.CurrentScene;
        public int MaxGameLevel => GameData.CountLevels;
        
        public MainMenuPresenter(MainMenuView view, GameData gameData, ViewManager viewManager) : base(view)
        {
            _gameData = gameData;
            _viewManager = viewManager;
            
            view.SubscribeEvents(this);
        }

        public void OnNewButtonPressed()
        {
            Debug.Log("Test");
        }
        
        public void OnContinueButtonPressed()
        {
        }
        
        public void OnHowPlayButtonPressed()
        {
            _viewManager.ShowView<HowPlayPresenter>();
        }
        
        public void OnSettingsButtonPressed()
        {
            _viewManager.ShowView<SettingsPresenter>();
        }
    }
}