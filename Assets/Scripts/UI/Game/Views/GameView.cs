using CBH.UI.Game.Presenters;
using ElectrumGames.MVP;
using TMPro;
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

        private const float TimeUpdateGps = 0.05f;
        private float _timePassed;

        private void Start()
        {
            Presenter.HeaderTextChanged += SetHeaderText;
            Presenter.TimerTextChanged += SetTimerText;
            buttonToMenu.onClick.AddListener(Presenter.OnToMenuClicked);
            
            UpdateGps();
        }

        private void FixedUpdate()
        {
            _timePassed += Time.fixedDeltaTime;
            if (_timePassed >= TimeUpdateGps)
            {
                UpdateGps();
                _timePassed = 0f;
            }
        }

        protected override void OnBeforeClose()
        {
            Presenter.HeaderTextChanged -= SetHeaderText;
            Presenter.TimerTextChanged -= SetTimerText;
            buttonToMenu.onClick.RemoveListener(Presenter.OnToMenuClicked);
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