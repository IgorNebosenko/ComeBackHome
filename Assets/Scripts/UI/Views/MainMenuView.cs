using CBH.UI.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Views
{
    [AutoRegisterView]
    public class MainMenuView : View<MainMenuPresenter>
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button howPlayButton;
        [SerializeField] private Button settingsButton;

        [SerializeField] private TMP_Text textProgress;

        private void OnEnable()
        {
            newGameButton.onClick.AddListener(Presenter.OnNewButtonPressed);
            continueButton.onClick.AddListener(Presenter.OnContinueButtonPressed);
            howPlayButton.onClick.AddListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.AddListener(Presenter.OnSettingsButtonPressed);

            continueButton.interactable = Presenter.CurrentLevel != 0;
            textProgress.text = $"Progress: {Presenter.CurrentLevel}/{Presenter.MaxGameLevel}";
        }

        private void OnDisable()
        {
            newGameButton.onClick.RemoveListener(Presenter.OnNewButtonPressed);
            continueButton.onClick.RemoveListener(Presenter.OnContinueButtonPressed);
            howPlayButton.onClick.RemoveListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonPressed);
        }
    }
}