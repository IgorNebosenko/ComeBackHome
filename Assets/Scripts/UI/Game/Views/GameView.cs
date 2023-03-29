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
        [SerializeField] private Image arrowImage;
        [SerializeField] private RectTransform pivotArrow;
        [SerializeField] private Button buttonToMenu;
        [Space] 
        [SerializeField] private GameObject leftBoostButton;
        [SerializeField] private GameObject rightBoostButton;

        private void Start()
        {
            Presenter.HeaderTextChanged += SetHeaderText;
            Presenter.TimerTextChanged += SetTimerText;
            buttonToMenu.onClick.AddListener(Presenter.OnToMenuClicked);
            
            leftBoostButton.SetActive(!Presenter.IsRightPositionBoost);
            rightBoostButton.SetActive(Presenter.IsRightPositionBoost);
            
            UpdateGps();
        }

        private void Update()
        {
            UpdateGps();
        }

        protected override void OnBeforeClose()
        {
            Presenter.HeaderTextChanged -= SetHeaderText;
            Presenter.TimerTextChanged -= SetTimerText;
            buttonToMenu.onClick.RemoveListener(Presenter.OnToMenuClicked);
        }
        
        private void UpdateGps()
        {
            Presenter.UpdateGps(arrowImage, pivotArrow);
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