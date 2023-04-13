using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView]
    public class MainMenuView : View<MainMenuPresenter>
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button selectLevelButton;
        [SerializeField] private Button howPlayButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button noAdsButton;

        [SerializeField] private TMP_Text textProgress;

        private void Start()
        {
            playButton.onClick.AddListener(Presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.AddListener(Presenter.OnLevelSelectButtonPressed);
            howPlayButton.onClick.AddListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.AddListener(Presenter.OnSettingsButtonPressed);
            noAdsButton.onClick.AddListener(Presenter.OnNoAdsButtonPressed);
            
            textProgress.text = $"Progress: {Presenter.CurrentLevel + 1}/{Presenter.MaxGameLevel}";
        }

        protected override void OnBeforeClose()
        {
            playButton.onClick.RemoveListener(Presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.RemoveListener(Presenter.OnLevelSelectButtonPressed);
            howPlayButton.onClick.RemoveListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonPressed);
            noAdsButton.onClick.RemoveListener(Presenter.OnNoAdsButtonPressed);
        }
    }
}