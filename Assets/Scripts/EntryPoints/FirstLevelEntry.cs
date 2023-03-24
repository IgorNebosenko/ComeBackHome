using CBH.Core.Audio;
using CBH.Core.Misc;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class FirstLevelEntry : MonoBehaviour
    {
        [SerializeField] private MusicClip musicClip = MusicClip.MainTheme;
        [SerializeField] private Vector3 gameGravity = Vector3.down * 40;
        
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
            AudioHandler.PlayMusicClip(musicClip);
            Physics.gravity = gameGravity;
            
            _viewManager.ShowView<GamePresenter>();

            if (_tutorialHandler.IsNeedShowTutorial())
                _popupManager.ShowPopup<TutorialPopupPresenter>();
        }
    }
}