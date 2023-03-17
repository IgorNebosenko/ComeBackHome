using CBH.Core.Misc;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class FirstLevelEntry : MonoBehaviour
    {
        private ViewManager _viewManager;
        private PopupManager _popupManager;
        private TutorialHandler _tutorialHandler;
        
        [Inject]
        private void Construct(ViewManager viewManager, PopupManager popupManager, TutorialHandler tutorialHandler)
        {
            _viewManager = viewManager;
            _popupManager = popupManager;
            _tutorialHandler = tutorialHandler;
        }

        private void Start()
        {
            _viewManager.ShowView<GamePresenter>();

            if (_tutorialHandler.IsNeedShowTutorial())
                _popupManager.ShowPopup<TutorialPopupPresenter>();
        }
    }
}