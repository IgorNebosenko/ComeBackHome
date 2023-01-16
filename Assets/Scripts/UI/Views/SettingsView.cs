using CBH.UI.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Views
{
    [AutoRegisterView]
    public class SettingsView : View<SettingsPresenter>
    {
        [SerializeField] private Toggle enableMusic;
        [SerializeField] private Toggle enableSounds;
        [SerializeField] private Slider fpsSlider;
        [SerializeField] private Button buttonExit;
        
        public TMP_Text fpsText;

        public void Init(SettingsPresenter presenter, SettingsInitData initData)
        {
            enableMusic.isOn = initData.enableMusic;
            enableSounds.isOn = initData.enableSounds;

            fpsSlider.value = initData.fpsSliderIndex;
            fpsText.text = initData.fpsData.name;
            fpsSlider.maxValue = initData.fpsSliderMaxValue;
            
            enableMusic.onValueChanged.AddListener(presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.AddListener(presenter.OnSoundsStateChanged);
            fpsSlider.onValueChanged.AddListener(presenter.OnSliderValueChanged);
            buttonExit.onClick.AddListener(presenter.OnButtonExitPressed);
        }

        private void OnDestroy()
        {
            enableMusic.onValueChanged.RemoveListener(Presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.RemoveListener(Presenter.OnSoundsStateChanged);
            fpsSlider.onValueChanged.RemoveListener(Presenter.OnSliderValueChanged);
            buttonExit.onClick.RemoveListener(Presenter.OnButtonExitPressed);
        }
    }
}