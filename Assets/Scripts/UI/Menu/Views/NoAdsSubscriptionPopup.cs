using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView("Views/Popups/NoAdsSubscriptionPopup")]
    public class NoAdsSubscriptionPopup : View<NoAdsSubscriptionPresenter>
    {
        [SerializeField] private Button buttonBuy;
        [SerializeField] private Button buttonClose;
        [SerializeField] private GameObject pendingPlaceholder;
        [SerializeField] private GameObject subscribedPlaceholder;
        [SerializeField] private TMP_Text textCost;

        private void Start()
        {
            Presenter.SendEventShowPopup();
            
            buttonBuy.onClick.AddListener(Presenter.OnButtonBuyPressed);
            buttonBuy.onClick.AddListener(SwapToPendingButton);
            buttonClose.onClick.AddListener(Presenter.OnButtonClosePressed);

            textCost.text = $"{Presenter.CostLocalized} per month";

            SetSubscriptionVisualStatus(Presenter.HasNoAds);
            Presenter.SubscriptionStatusChanged += SetSubscriptionVisualStatus;
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