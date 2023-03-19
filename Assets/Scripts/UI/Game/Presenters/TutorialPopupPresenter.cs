using System.Collections.Generic;
using CBH.Core.Misc;
using CBH.UI.Game.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;

namespace CBH.UI.Game.Presenters
{
    public class TutorialPopupPresenter : PopupPresenterCoroutine<TutorialPopup, PopupArgs, PopupResult>
    {
        private TutorialHandler _tutorialHandler;
        
        public TutorialPopupPresenter(TutorialPopup view, TutorialHandler tutorialHandler) : base(view)
        {
            _tutorialHandler = tutorialHandler;
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield return null;
        }

        public void OnFadeZoneClicked()
        {
            _tutorialHandler.SetCompletedState();
            Close();
        }
    }
}