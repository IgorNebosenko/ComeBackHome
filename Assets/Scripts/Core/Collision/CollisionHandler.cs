using System;
using CBH.Core.Audio;
using CBH.Core.Entity;
using UnityEngine;
using Zenject;

namespace CBH.Core.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private RocketManager rocketManager;

        [SerializeField] private ParticleSystem deadthRocketParticle;
        [SerializeField] private ParticleSystem successParticle;

        private AudioManager _audioManager;
        private bool _isCalledTick;

        private void Awake()
        {
            rocketManager = GetComponent<RocketManager>();
        }

        [Inject]
        private void Inject(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(CollisionObjectBase), out var obj))
            {
                if (obj is ObstacleCollisionObject)
                    HandleObstacleCollision();
                else if (obj is FinishCollisionObject)
                    HandleFinishCollision();
                
                if (!_isCalledTick && obj is not SafeCollisionObject)
                {
                    _isCalledTick = true;
                    RocketManager.TimerStarter.Invoke();
                }
            }
        }

        private void HandleObstacleCollision()
        {
            rocketManager.currentRocketState = RocketManager.RocketState.Dead;
            _audioManager.
            _audioSource.Stop();
            _audioSource.PlayOneShot(deadthRocketSFX);
            deadthRocketParticle.Play();
            GetComponent<Movement>().enabled = false;
        }

        private void HandleFinishCollision()
        {
            if (rocketManager.currentRocketState != RocketManager.RocketState.Win)
            {
                rocketManager.currentRocketState = RocketManager.RocketState.Win;
                _audioSource.Stop();
                _audioSource.PlayOneShot(successSFX);
                successParticle.Play();
                GetComponent<Movement>().enabled = false;
            }
        }
    }
}