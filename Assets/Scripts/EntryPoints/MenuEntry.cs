using CBH.Core.Misc;
using CBH.UI.Menu.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CBH.EntryPoints
{
    public class MenuEntry : MonoBehaviour
    {
        private ViewManager _viewManager;
        private TutorialHandler _tutorialHandler;
        
        [Inject]
        private void Construct(ViewManager viewManager, TutorialHandler tutorialHandler)
        {
            _viewManager = viewManager;
            _tutorialHandler = tutorialHandler;
        }

        private void Awake()
        {
            if (_tutorialHandler.IsNeedShowTutorial())
                SceneManager.LoadScene(1);
            else
                _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}
