using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    public class LevelSelectView : View<LevelSelectPresenter>
    {
        [SerializeField] private Button toMenuButton;

        public void Init(LevelSelectPresenter presenter)
        {
            toMenuButton.onClick.AddListener(presenter.OnToMenuClicked);
        }

        private void OnDestroy()
        {
            toMenuButton.onClick.RemoveListener(Presenter.OnToMenuClicked);
        }
    }
}