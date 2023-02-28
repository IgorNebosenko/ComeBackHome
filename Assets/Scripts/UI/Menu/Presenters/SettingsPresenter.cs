using CBH.Core;
using CBH.Core.Audio;
using CBH.Core.Configs;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;

namespace CBH.UI.Menu.Presenters
{
    public class SettingsPresenter : Presenter<SettingsView>
    {
        private FpsConfig _fpsConfig;
        private GameData _gameData;
        private AudioManager _audioManager;
        private ViewManager _viewManager;
        
        public SettingsInitData SettingsInitData { get; private set; }
        
        public SettingsPresenter(SettingsView view, FpsConfig fpsConfig, AudioManager audioManager, 
            GameData gameData, ViewManager viewManager) :
            base(view)
        {
            _fpsConfig = fpsConfig;
            _gameData = gameData;
            _audioManager = audioManager;
            _viewManager = viewManager;

            var fpsIndex = -1;
            for (var i = 0; i < _fpsConfig.config.Length; i++)
            {
                if (_fpsConfig.config[i].fps != _gameData.TargetFps) 
                    continue;
                
                fpsIndex = i;
                break;
            }

            SettingsInitData = new SettingsInitData(_audioManager.EnableMusic, _audioManager.EnableSounds, 
                    fpsIndex, _fpsConfig.config[fpsIndex], _fpsConfig.config.Length - 1);
        }

        public void OnMusicStateChanged(bool state)
        {
            _audioManager.EnableMusic = state;
        }
        
        public void OnSoundsStateChanged(bool state)
        {
            _audioManager.EnableSounds = state;
        }

        public void OnSliderValueChanged(float index)
        {
            var fpsData = _fpsConfig.config[(int)index];
            
            _gameData.UpdateTargetFps(fpsData.fps);
            View.fpsText.text = fpsData.name;
        }

        public void OnButtonExitPressed()
        {
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }

    public readonly struct SettingsInitData
    {
        public readonly bool enableMusic;
        public readonly bool enableSounds;
        public readonly int fpsSliderIndex;
        public readonly FpsConfigData fpsData;
        public readonly int fpsSliderMaxValue;
        
        public SettingsInitData(bool enableMusic, bool enableSounds, int fpsSliderIndex, FpsConfigData fpsData,
            int fpsSliderMaxValue)
        {
            this.enableMusic = enableMusic;
            this.enableSounds = enableSounds;
            this.fpsSliderIndex = fpsSliderIndex;
            this.fpsData = fpsData;
            this.fpsSliderMaxValue = fpsSliderMaxValue;
        }
    }
}