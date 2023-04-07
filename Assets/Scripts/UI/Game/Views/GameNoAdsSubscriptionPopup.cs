using System;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CBH.UI.Game.Views
{
    [AutoRegisterView("Views/Popups/GameNoAdsSubscriptionPopup")]
    public class GameNoAdsSubscriptionPopup : View<GameNoAdsSubscriptionPresenter>
    {
        [SerializeField] private Button buttonBuy;
        [SerializeField] private Button buttonClose;
        [SerializeField] private GameObject pendingPlaceholder;
        [SerializeField] private GameObject subscribedPlaceholder;
        [SerializeField] private TMP_Text textCost;

        private const float TimeBeforeCloseEnable = 3f;

        private void Start()
        {
            buttonBuy.onClick.AddListener(Presenter.OnButtonBuyPressed);
            buttonBuy.onClick.AddListener(SwapToPendingButton);
            buttonClose.onClick.AddListener(Presenter.OnButtonClosePressed);

            textCost.text = $"{Presenter.CostLocalized} per month";

            SetSubscriptionVisualStatus(Presenter.HasNoAds);
            Presenter.SubscriptionStatusChanged += SetSubscriptionVisualStatus;

            Observable.Timer(TimeSpan.FromSeconds(TimeBeforeCloseEnable)).Subscribe(
                _ => buttonClose.gameObject.SetActive(true)).AddTo(this);
        }

        protected override void OnBeforeClose()
        {
            buttonBuy.onClick.RemoveListener(Presenter.OnButtonBuyPressed);
            buttonBuy.onClick.RemoveListener(SwapToPendingButton);
            buttonClose.onClick.RemoveListener(Presenter.OnButtonClosePressed);

            Presenter.SubscriptionStatusChanged -= SetSubscriptionVisualStatus;
        }

        private void SetSubscriptionVisualStatus(bool status)
        {
            buttonBuy.gameObject.SetActive(!status);
            pendingPlaceholder.gameObject.SetActive(false);
            subscribedPlaceholder.SetActive(status);
        }

        private void SwapToPendingButton()
        {
            buttonBuy.gameObject.SetActive(false);
            pendingPlaceholder.gameObject.SetActive(true);
            subscribedPlaceholder.SetActive(false);
        }
    }
}