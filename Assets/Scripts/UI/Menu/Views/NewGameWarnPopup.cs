using CBH.UI.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Views
{
    [AutoRegisterView("Views/Popups/NewGameWarnPopup")]
    public class NewGameWarnPopup : View<NewGameWarnPresenter>
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backButton;

        public void SubscribeEvents(NewGameWarnPresenter presenter)
        {
            restartButton.onClick.AddListener(presenter.RestartButtonClicked);
            backButton.onClick.AddListener(presenter.Close);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(Presenter.RestartButtonClicked);
            backButton.onClick.RemoveListener(Presenter.Close);
        }
    }
}