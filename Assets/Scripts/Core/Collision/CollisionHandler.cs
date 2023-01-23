using CBH.Core.Entity;
using UnityEngine;
using Zenject;

namespace CBH.Core.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        private GameManager _gameManager;

        private bool _isCalledTick;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var other = collision.collider;
            
            if (other.TryGetComponent(typeof(CollisionObjectBase), out var obj))
            {
                if (obj is ObstacleCollisionObject)
                    HandleObstacleCollision();
                else if (obj is FinishCollisionObject)
                    HandleFinishCollision();
                
                if (!_isCalledTick && obj is not SafeCollisionObject)
                {
                    _isCalledTick = true;
                    _gameManager.HandleRocketState(RocketState.Dead);
                }
            }
        }

        private void HandleObstacleCollision()
        {
            _gameManager.HandleRocketState(RocketState.Dead);
        }

        private void HandleFinishCollision()
        {
            if (_gameManager.CurrentState == RocketState.Win) 
                return;
            
            _gameManager.HandleRocketState(RocketState.Win);
        }
    }
}