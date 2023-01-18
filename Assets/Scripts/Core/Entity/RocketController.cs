using UnityEngine;
using Zenject;

namespace CBH.Core.Entity
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField] private Rigidbody physicModel;

        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;

            _gameManager.OnLoadNextLevel += OnLoadNextLevel;
        }

        private void OnDestroy()
        {
            _gameManager.OnLoadNextLevel -= OnLoadNextLevel;
        }

        private void OnLoadNextLevel()
        {
            physicModel.freezeRotation = true;
            physicModel.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}