using CBH.Core.Audio;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private MusicClip musicClip = MusicClip.MainTheme;
        [SerializeField] private Vector3 gameGravity = Vector3.down * 40;
        
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Start()
        {
            AudioHandler.PlayMusicClip(musicClip);
            Physics.gravity = gameGravity;
            
            _viewManager.ShowView<GamePresenter>();
        }
    }
}