using CBH.UI.Menu.Presenters;
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
        [Space]
        [SerializeField] private Slider fpsSlider;
        [SerializeField] private TMP_Text fpsText;
        [Space] 
        [SerializeField] private Toggle leftButtonBoostToggle;
        [Space]
        [SerializeField] private Button buttonExit;

        private void Start()
        {
            enableMusic.isOn = Presenter.InitData.enableMusic;
            enableSounds.isOn = Presenter.InitData.enableSounds;

            fpsSlider.value = Presenter.InitData.fpsSliderIndex;
            fpsText.text = Presenter.InitData.fpsData.name;
            fpsSlider.maxValue = Presenter.InitData.fpsSliderMaxValue;
            
            enableMusic.onValueChanged.AddListener(Presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.AddListener(Presenter.OnSoundsStateChanged);
            fpsSlider.onValueChanged.AddListener((value) => Presenter.OnSliderValueChanged(value, SetFpsText));
            
            leftButtonBoostToggle.onValueChanged.AddListener(Presenter.OnChangeBoostPositionClicked);
            leftButtonBoostToggle.isOn = Presenter.IsRightPositionBoost;
            
            buttonExit.onClick.AddListener(Presenter.OnButtonExitPressed);
        }

        protected override void OnBeforeClose()
        {
            enableMusic.onValueChanged.RemoveListener(Presenter.OnMusicStateChanged);
            enableSounds.onValueChanged.RemoveListener(Presenter.OnSoundsStateChanged);
            leftButtonBoostToggle.onValueChanged.RemoveListener(Presenter.OnChangeBoostPositionClicked);
            fpsSlider.onValueChanged.RemoveListener((value) => Presenter.OnSliderValueChanged(value, SetFpsText));
            buttonExit.onClick.RemoveListener(Presenter.OnButtonExitPressed);
        }

        private void SetFpsText(string text)
        {
            fpsText.text = text;
        }
    }
}