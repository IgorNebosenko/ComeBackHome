using System.Collections.Generic;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;

namespace CBH.UI.Menu.Presenters
{
    public class NoAdsSubscriptionPresenter : PopupPresenterCoroutine<NoAdsSubscriptionPopup, PopupArgs, PopupResult>
    {
        public NoAdsSubscriptionPresenter(NoAdsSubscriptionPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}