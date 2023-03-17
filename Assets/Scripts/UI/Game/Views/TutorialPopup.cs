using CBH.UI.Game.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Game.Views
{
    [AutoRegisterView("Views/Popups/TutorialPopup")]
    public class TutorialPopup : View<TutorialPopupPresenter>
    {
        [SerializeField] private Button fadeCloseZone;

        private void Start()
        {
            fadeCloseZone.onClick.AddListener(Presenter.OnFadeZoneClicked);
        }

        private void OnDestroy()
        {
            fadeCloseZone.onClick.RemoveListener(Presenter.OnFadeZoneClicked);
        }
    }
}