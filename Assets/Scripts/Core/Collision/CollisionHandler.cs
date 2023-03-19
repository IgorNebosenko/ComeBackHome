using CBH.Core.Entity;
using UnityEngine;
using Zenject;

namespace CBH.Core.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (_gameManager.CurrentState != RocketState.Live)
                return;
            
            var other = collision.collider;
            
            if (other.TryGetComponent(typeof(CollisionObjectBase), out var obj))
            {
                if (obj is ObstacleCollisionObject)
                    HandleObstacleCollision();
                else if (obj is FinishCollisionObject)
                    HandleFinishCollision();
            }
        }

        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            if (_gameManager.CurrentState != RocketState.Win && _gameManager.CurrentState != RocketState.LandFinishPad)
                return;
            
            var other = collision.collider;
            
            if (other.TryGetComponent(typeof(FinishCollisionObject), out var obj))
                _gameManager.HandleRocketState(RocketState.LeaveFinishPad);
        }

        private void HandleObstacleCollision()
        {
            _gameManager.HandleRocketState(RocketState.Dead);
        }

        private void HandleFinishCollision()
        {
            if (_gameManager.CurrentState == RocketState.LandFinishPad) 
                return;
            
            _gameManager.HandleRocketState(RocketState.LandFinishPad);
        }
    }
}