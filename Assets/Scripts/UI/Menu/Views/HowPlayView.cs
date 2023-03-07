using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView]
    public class HowPlayView : View<HowPlayPresenter>
    {
        [SerializeField] private Button buttonClose;

        private void Start()
        {
            buttonClose.onClick.AddListener(Presenter.OnButtonBackPressed);
        }

        protected override void OnBeforeClose()
        {
            buttonClose.onClick.RemoveListener(Presenter.OnButtonBackPressed);
        }
    }
}