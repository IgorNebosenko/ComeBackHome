﻿using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class GameEntry : MonoBehaviour
    {
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Start()
        {
            _viewManager.ShowView<GamePresenter>();
        }
    }
}