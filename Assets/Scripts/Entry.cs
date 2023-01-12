using System;
using CBH.UI.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.Core
{
    public class Entry : MonoBehaviour
    {
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Awake()
        {
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}
