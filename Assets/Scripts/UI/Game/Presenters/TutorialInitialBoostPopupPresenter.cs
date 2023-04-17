using System.Collections.Generic;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;

namespace CBH.UI.Game.Presenters
{
    public class TutorialInitialBoostPopupPresenter : PopupPresenterCoroutine<TutorialInitialBoostPopup, PopupArgs, PopupResult>
    {
        public TutorialInitialBoostPopupPresenter(TutorialInitialBoostPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield return null;
        }

        public void OnFadeClicked()
        {
            Close();
        }
    }
}