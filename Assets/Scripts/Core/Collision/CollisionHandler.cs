using CBH.Core.Entity;
using UnityEngine;
using Zenject;

namespace CBH.Core.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        private RocketManager _rocketManager;
        private GameManager _gameManager;

        private bool _isCalledTick;

        [Inject]
        private void Construct(RocketManager rocketManager, GameManager gameManager)
        {
            _rocketManager = rocketManager;
            _gameManager = gameManager;
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
                    _gameManager.HandleRocketState(RocketState.Dead);
                }
            }
        }

        private void HandleObstacleCollision()
        {
            _rocketManager.SetRocketState(RocketState.Dead);
        }

        private void HandleFinishCollision()
        {
            if (_rocketManager.CurrentRocketState == RocketState.Win) 
                return;
            
            _rocketManager.SetRocketState(RocketState.Win);
        }
    }
}