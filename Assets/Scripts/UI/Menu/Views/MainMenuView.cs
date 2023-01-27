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
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button howPlayButton;
        [SerializeField] private Button settingsButton;

        [SerializeField] private TMP_Text textProgress;

        public void SubscribeEvents(MainMenuPresenter presenter)
        {
            newGameButton.onClick.AddListener(presenter.OnNewButtonPressed);
            continueButton.onClick.AddListener(presenter.OnContinueButtonPressed);
            howPlayButton.onClick.AddListener(presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.AddListener(presenter.OnSettingsButtonPressed);

            continueButton.interactable = presenter.CurrentLevel > 1;
            textProgress.text = $"Progress: {presenter.CurrentLevel}/{presenter.MaxGameLevel}";
        }

        private void OnDestroy()
        {
            newGameButton.onClick.RemoveListener(Presenter.OnNewButtonPressed);
            continueButton.onClick.RemoveListener(Presenter.OnContinueButtonPressed);
            howPlayButton.onClick.RemoveListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonPressed);
        }
    }
}