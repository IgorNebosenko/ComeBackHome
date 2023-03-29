using System;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.Core.Audio;
using CBH.Core.Configs;
using CBH.Core.Core.Misc;
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
        private GlobalUserSettings _globalUserSettings;
        private ViewManager _viewManager;

        private IAnalyticsManager _analyticsManager;
        
        public SettingsInitData InitData { get; }
        public bool IsRightPositionBoost => _globalUserSettings.IsRightPositionBoost;
        
        public SettingsPresenter(SettingsView view, FpsConfig fpsConfig, AudioManager audioManager, 
            GameData gameData, GlobalUserSettings globalUserSettings, IAnalyticsManager analyticsManager, 
            ViewManager viewManager) : base(view)
        {
            _fpsConfig = fpsConfig;
            _gameData = gameData;
            _audioManager = audioManager;
            _globalUserSettings = globalUserSettings;
            _viewManager = viewManager;
            _analyticsManager = analyticsManager;

            var fpsIndex = -1;
            for (var i = 0; i < _fpsConfig.config.Length; i++)
            {
                if (_fpsConfig.config[i].fps != _gameData.TargetFps) 
                    continue;
                
                fpsIndex = i;
                break;
            }

            InitData = new SettingsInitData(_audioManager.EnableMusic, _audioManager.EnableSounds, 
                    fpsIndex, _fpsConfig.config[fpsIndex], _fpsConfig.config.Length - 1);
        }

        public void OnMusicStateChanged(bool state)
        {
            _audioManager.EnableMusic = state;
            _analyticsManager.SendEvent(new ChangeEnableMusicEvent(state));
        }
        
        public void OnSoundsStateChanged(bool state)
        {
            _audioManager.EnableSounds = state;
            _analyticsManager.SendEvent(new ChangeEnableSoundsEvent(state));
        }

        public void OnSliderValueChanged(float index, Action<string> onComplete)
        {
            var fpsData = _fpsConfig.config[(int)index];
            
            _gameData.UpdateTargetFps(fpsData.fps);
            _analyticsManager.SendEvent(new ChangeTargetFpsEvent(fpsData.fps));
            
            onComplete?.Invoke(fpsData.name);
        }

        public void OnChangeBoostPositionClicked(bool state)
        {
            _globalUserSettings.IsRightPositionBoost = state;
            _analyticsManager.SendEvent(new ChangeBoostPositionEvent(state));
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