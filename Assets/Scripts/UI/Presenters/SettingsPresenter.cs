using CBH.Core;
using CBH.Core.Configs;
using CBH.UI.Views;
using ElectrumGames.MVP;

namespace CBH.UI.Presenters
{
    public class SettingsPresenter : Presenter<SettingsView>
    {
        private FpsConfig _fpsConfig;
        private GameData _gameData;
        
        public SettingsPresenter(SettingsView view, FpsConfig fpsConfig, GameData gameData) : base(view)
        {
            _fpsConfig = fpsConfig;
            _gameData = gameData;
        }
    }
}