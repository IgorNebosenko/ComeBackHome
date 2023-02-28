﻿using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView]
    public class SettingsView : View<SettingsPresenter>
    {
        [SerializeField] private Toggle enableMusic;
        [SerializeField] private Toggle enableSounds;
        [SerializeField] private Slider fpsSlider;
        [SerializeField] private Button buttonExit;
        
        public TMP_Text fpsText;

        private void Start()
        {
            enableMusic.isOn = Presenter.SettingsInitData.enableMusic;
            enableSounds.isOn = Presenter.SettingsInitData.enableSounds;

            fpsSlider.value = Presenter.SettingsInitData.fpsSliderIndex;
            fpsText.text = Presenter.SettingsInitData.fpsData.name;
            fpsSlider.maxValue = Presenter.SettingsInitData.fpsSliderMaxValue;
            
            enableMusic.onValueChanged.AddListener(Presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.AddListener(Presenter.OnSoundsStateChanged);
            fpsSlider.onValueChanged.AddListener(Presenter.OnSliderValueChanged);
            buttonExit.onClick.AddListener(Presenter.OnButtonExitPressed);
        }

        protected override void OnBeforeClose()
        {
            enableMusic.onValueChanged.RemoveListener(Presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.RemoveListener(Presenter.OnSoundsStateChanged);
            fpsSlider.onValueChanged.RemoveListener(Presenter.OnSliderValueChanged);
            buttonExit.onClick.RemoveListener(Presenter.OnButtonExitPressed);
        }
    }
}