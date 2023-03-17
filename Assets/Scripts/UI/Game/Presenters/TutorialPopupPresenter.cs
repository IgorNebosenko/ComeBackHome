using System.Collections.Generic;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;

namespace CBH.UI.Game.Presenters
{
    public class TutorialPopupPresenter : PopupPresenterCoroutine<TutorialPopup, PopupArgs, PopupResult>
    {
        public TutorialPopupPresenter(TutorialPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield return null;
        }

        public void OnFadeZoneClicked()
        {
            Close();
        }
    }
}