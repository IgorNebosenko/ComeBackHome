using CBH.Core.Levels;
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
        private ILevelsManager _levelsManager;
        
        [Inject]
        private void Construct(ViewManager viewManager, TutorialHandler tutorialHandler, ILevelsManager levelsManager)
        {
            _viewManager = viewManager;
            _tutorialHandler = tutorialHandler;
            _levelsManager = levelsManager;
        }

        private void Awake()
        {
            if (_tutorialHandler.IsNeedShowTutorial())
                _levelsManager.LoadTutorial();
            else
                _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}
