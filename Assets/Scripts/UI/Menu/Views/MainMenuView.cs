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
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button noAdsButton;

        [SerializeField] private TMP_Text textProgress;

        private void Start()
        {
            playButton.onClick.AddListener(Presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.AddListener(Presenter.OnLevelSelectButtonPressed);
            tutorialButton.onClick.AddListener(Presenter.OnTutorialButtonPressed);
            settingsButton.onClick.AddListener(Presenter.OnSettingsButtonPressed);
            quitButton.onClick.AddListener(Presenter.OnQuitButtonPressed);
            noAdsButton.onClick.AddListener(Presenter.OnNoAdsButtonPressed);
            
            textProgress.text = $"Progress: {Presenter.CurrentLevel + 1}/{Presenter.MaxGameLevel}";
        }

        protected override void OnBeforeClose()
        {
            playButton.onClick.RemoveListener(Presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.RemoveListener(Presenter.OnLevelSelectButtonPressed);
            tutorialButton.onClick.RemoveListener(Presenter.OnTutorialButtonPressed);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonPressed);
            quitButton.onClick.RemoveListener(Presenter.OnQuitButtonPressed);
            noAdsButton.onClick.RemoveListener(Presenter.OnNoAdsButtonPressed);
        }
    }
}