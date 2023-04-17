using CBH.UI.Game.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Game.Views
{
    public class TutorialInitialBoostPopup : View<TutorialInitialBoostPopupPresenter>
    {
        [SerializeField] private Button fadeBackground;
        [SerializeField] private Rect pointer;

        private void Start()
        {
            fadeBackground.onClick.AddListener(Presenter.OnFadeClicked);
        }

        private void OnDestroy()
        {
            fadeBackground.onClick.RemoveListener(Presenter.OnFadeClicked);
        }

    }
}