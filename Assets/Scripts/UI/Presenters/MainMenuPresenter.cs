using CBH.Core;
using CBH.UI.Views;
using ElectrumGames.MVP;

namespace CBH.UI.Presenters
{
    public class MainMenuPresenter : Presenter<MainMenuView>
    {
        private GameData _gameData;

        public int CurrentLevel => _gameData.CurrentScene;
        public int MaxGameLevel => GameData.CountLevels;
        
        public MainMenuPresenter(MainMenuView view, GameData gameData) : base(view)
        {
            _gameData = gameData;
        }

        public void OnNewButtonPressed()
        {
        }
        
        public void OnContinueButtonPressed()
        {
        }
        
        public void OnHowPlayButtonPressed()
        {
        }
        
        public void OnSettingsButtonPressed()
        {
        }
    }
}