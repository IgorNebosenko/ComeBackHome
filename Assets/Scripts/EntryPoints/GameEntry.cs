using CBH.Core;
using CBH.Core.Audio;
using CBH.Core.Configs.Levels;
using CBH.UI.Game.Presenters;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace CBH.EntryPoints
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private Transform visualParent;
        [SerializeField] private Transform launchPadTransform;
        [SerializeField] private Transform landingPadTransform;
        [SerializeField] private Transform rocketTransform;

        private ViewManager _viewManager;
        private GameManager _gameManager;
        private LevelsConfig _levelsConfig;
        
        [Inject]
        private void Construct(ViewManager viewManager, GameManager gameManager, LevelsConfig levelsConfig)
        {
            _viewManager = viewManager;
            _gameManager = gameManager;
            _levelsConfig = levelsConfig;
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