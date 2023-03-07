using System;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Game.Views
{
    [AutoRegisterView]
    public class GameView : View<GamePresenter>
    {
        [SerializeField] private TMP_Text textHeader;
        [SerializeField] private TMP_Text textTimer;
        [SerializeField] private Image gpsImage;
        [SerializeField] private Button buttonToMenu;

        private IDisposable _updateGpsProcess;

        private void Start()
        {
            Presenter.HeaderTextChanged += SetHeaderText;
            Presenter.TimerTextChanged += SetTimerText;
            buttonToMenu.onClick.AddListener(Presenter.OnToMenuClicked);
            
            _updateGpsProcess = Observable.EveryLateUpdate().Subscribe(_ => UpdateGps());
        }

        protected override void OnBeforeClose()
        {
            Presenter.HeaderTextChanged -= SetHeaderText;
            Presenter.TimerTextChanged -= SetTimerText;
            buttonToMenu.onClick.RemoveListener(Presenter.OnToMenuClicked);
            
            _updateGpsProcess?.Dispose();
        }

        private void UpdateGps()
        {
            Presenter.UpdateGps(gpsImage);
        }

        private void SetHeaderText(string text)
        {
            textHeader.text = text;
        }

        private void SetTimerText(string text)
        {
            textTimer.text = text;
        }
    }
}