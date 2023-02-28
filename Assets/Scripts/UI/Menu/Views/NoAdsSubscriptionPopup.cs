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
        [SerializeField] private RectTransform popupGroup;

        [SerializeField] private Button buttonBuy;
        [SerializeField] private Button buttonClose;
        [SerializeField] private TMP_Text textCost;

        private void Start()
        {
            Presenter.SendEventShowPopup();
            
            buttonBuy.onClick.AddListener(Presenter.OnButtonBuyPressed);
            buttonClose.onClick.AddListener(Presenter.OnButtonClosePressed);

            textCost.text = $"{Presenter.CostLocalized} per month";
        }

        protected override void OnBeforeClose()
        {
            buttonBuy.onClick.RemoveListener(Presenter.OnButtonBuyPressed);
            buttonClose.onClick.RemoveListener(Presenter.OnButtonClosePressed);
        }
    }
}