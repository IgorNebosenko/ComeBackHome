using System.Collections.Generic;
using CBH.Core.Audio;
using CBH.Core.Configs;
using CBH.Core.Core.Entity.Motors;
using CBH.Core.Core.Entity.Visual;
using UnityEngine;
using Zenject;

namespace CBH.Core.Entity.Input
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem leftParticleSystem;
        [SerializeField] private ParticleSystem rightParticleSystem;
        [SerializeField] private ParticleSystem boostEffect;
        [Space]
        [SerializeField] private ParticleSystem winEffect;
        [SerializeField] private ParticleSystem explosionEffect;
        [Space]
        [SerializeField] private Rigidbody physicModel;
        [SerializeField] private float trustPower = 5000f;
        [SerializeField] private float rotationSensitivity = 2000f;

        private GameManager _gameManager;
        private IInput _input;
        private bool _isInputMade;

        private RocketMotor _motor;
        private List<IEntityVisual> _entityVisualsList;

        private FinishGameEffect _finishGameEffect;

        public Vector3 ControllerPosition => transform.position;

        [Inject]
        private void Construct(GameManager gameManager, InputSchema inputSchema)
        {
            _gameManager = gameManager;
            
            _input = new RocketInput(inputSchema);
            _input.Init();
            
            _motor = new RocketMotor(physicModel, trustPower, rotationSensitivity);

            _entityVisualsList = new List<IEntityVisual>()
            {
                new SoundRocketEffects(),
                new ParticlesRocketEffects(leftParticleSystem, rightParticleSystem, boostEffect)
            };

            _finishGameEffect = new FinishGameEffect(winEffect, explosionEffect);

            _gameManager.BeforeWin += OnBeforeWin;
            _gameManager.LevelWin += _finishGameEffect.PlayWin;
            _gameManager.LevelLose += _finishGameEffect.PlayLose;
        }

        private void FixedUpdate()
        {
            if (_gameManager.CurrentState != RocketState.Live &&
                _gameManager.CurrentState != RocketState.LandFinishPad)
                return;
            
            var deltaTime = Time.fixedDeltaTime;
            
            _input.Update(deltaTime);
            HandleFirstInput();
            
            _motor.Simulate(deltaTime, _input);

            foreach (var visualEffect in _entityVisualsList)
                visualEffect.Simulate(_input);
        }

        private void OnDestroy()
        {
            _gameManager.BeforeWin -= OnBeforeWin;
            _gameManager.LevelWin -= _finishGameEffect.PlayWin;
            _gameManager.LevelLose -= _finishGameEffect.PlayLose;
        }

        private void OnBeforeWin()
        {
            AudioHandler.StopLoopSound();
            physicModel.freezeRotation = true;
            physicModel.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void HandleFirstInput()
        {
            if (_isInputMade)
                return;

            if (_input.EnabledBoost || _input.RotationDirection != 0)
            {
                _isInputMade = true;
                _gameManager.StartFlyProcess();
            }
        }
    }
}