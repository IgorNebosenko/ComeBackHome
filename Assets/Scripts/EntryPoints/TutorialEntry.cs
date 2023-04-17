using CBH.Core.Audio;
using CBH.Core.Configs.Levels;
using CBH.Core.Levels;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class TutorialEntry : MonoBehaviour
    {
        [SerializeField] private Transform visualParent;
        [SerializeField] private Transform launchPadTransform;
        [SerializeField] private Transform landingPadTransform;
        [SerializeField] private Transform rocketTransform;

        private ViewManager _viewManager;
        private ILevelsManager _levelsManager;
        
        [Inject]
        private void Construct(ViewManager viewManager, ILevelsManager levelsManager)
        {
            _viewManager = viewManager;
            _levelsManager = levelsManager;
            
            Init(levelsManager.CurrentLevel.levelDataConfig);
        }

        public void Init(LevelDataConfig config)
        {
            Instantiate(config.visual, visualParent);
            landingPadTransform.position = config.landingPadPosition;
            launchPadTransform.position = config.launchPadPosition;
            rocketTransform.position = config.rocketPosition;
            
            AudioHandler.PlayMusicClip(config.musicClip);
            Physics.gravity = config.gameGravity;
        }

        private void Start()
        {
            _viewManager.ShowView<GamePresenter>();
        }
    }
}