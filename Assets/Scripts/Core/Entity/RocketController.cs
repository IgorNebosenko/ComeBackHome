using System.Collections.Generic;
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
        [SerializeField] private float rotationPerSecond = 200f;

        private GameManager _gameManager;
        private IInput _input;

        private RocketMotor _motor;
        private List<IEntityVisual> _entityVisualsList;

        private FinishGameEffect _finishGameEffect;

        [Inject]
        private void Construct(GameManager gameManager, InputSchema inputSchema)
        {
            _gameManager = gameManager;
            
            _input = new RocketInput(inputSchema);
            _input.Init();
            
            _motor = new RocketMotor(physicModel, trustPower, rotationPerSecond);

            _entityVisualsList = new List<IEntityVisual>()
            {
                new SoundRocketEffects(),
                new ParticlesRocketEffects(leftParticleSystem, rightParticleSystem, boostEffect)
            };

            _finishGameEffect = new FinishGameEffect(winEffect, explosionEffect);

            _gameManager.OnLoadNextLevel += OnLoadNextLevel;
            _gameManager.OnLevelWin += _finishGameEffect.PlayWin;
            _gameManager.OnLevelLose += _finishGameEffect.PlayLose;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            _input.Update(deltaTime);
            
            _motor.Simulate(deltaTime, _input);

            foreach (var visualEffect in _entityVisualsList)
                visualEffect.Simulate(_input);
        }

        private void OnDestroy()
        {
            _gameManager.OnLoadNextLevel -= OnLoadNextLevel;
            _gameManager.OnLevelWin -= _finishGameEffect.PlayWin;
            _gameManager.OnLevelLose -= _finishGameEffect.PlayLose;
        }

        private void OnLoadNextLevel()
        {
            physicModel.freezeRotation = true;
            physicModel.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}