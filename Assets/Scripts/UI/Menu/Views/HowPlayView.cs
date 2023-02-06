﻿using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView]
    public class HowPlayView : View<HowPlayPresenter>
    {
        [SerializeField] private Button buttonClose;

        public void SubscribeEvents(HowPlayPresenter presenter)
        {
            buttonClose.onClick.AddListener(presenter.OnButtonBackPressed);
        }

        private void OnDestroy()
        {
            buttonClose.onClick.RemoveListener(Presenter.OnButtonBackPressed);
        }
    }
}