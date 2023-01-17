using CBH.Core.Audio;
using CBH.Core.Entity;
using UnityEngine;

namespace CBH.Core.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private RocketManager rocketManager;

        [SerializeField] private ParticleSystem deathRocketParticle;
        [SerializeField] private ParticleSystem successParticle;
        
        private bool _isCalledTick;

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
            AudioHandler.PlaySoundEffect(SoundEffect.Death);
            deathRocketParticle.Play();
            GetComponent<Movement>().enabled = false;
        }

        private void HandleFinishCollision()
        {
            if (rocketManager.currentRocketState != RocketManager.RocketState.Win)
            {
                rocketManager.currentRocketState = RocketManager.RocketState.Win;
                AudioHandler.PlaySoundEffect(SoundEffect.Success);
                successParticle.Play();
                GetComponent<Movement>().enabled = false;
            }
        }
    }
}