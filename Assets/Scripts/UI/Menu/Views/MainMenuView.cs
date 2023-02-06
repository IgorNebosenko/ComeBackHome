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

        [SerializeField] private TMP_Text textProgress;

        public void SubscribeEvents(MainMenuPresenter presenter)
        {
            playButton.onClick.AddListener(presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.AddListener(presenter.OnLevelSelectButtonPressed);
            howPlayButton.onClick.AddListener(presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.AddListener(presenter.OnSettingsButtonPressed);
            
            textProgress.text = $"Progress: {presenter.CurrentLevel}/{presenter.MaxGameLevel}";
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(Presenter.OnPlayButtonPressed);
            selectLevelButton.onClick.RemoveListener(Presenter.OnLevelSelectButtonPressed);
            howPlayButton.onClick.RemoveListener(Presenter.OnHowPlayButtonPressed);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonPressed);
        }
    }
}