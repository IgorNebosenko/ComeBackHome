using CBH.Core;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;

namespace CBH.UI.Menu.Presenters
{
    public class LevelSelectPresenter : Presenter<LevelSelectView>
    {
        private ViewManager _viewManager;
        private GameData _gameData;
        
        public LevelSelectPresenter(ViewManager viewManager, GameData gameData, LevelSelectView view) : base(view)
        {
            _viewManager = viewManager;
            _gameData = gameData;
            
            view.Init(this);
        }

        public void OnToMenuClicked()
        {
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}