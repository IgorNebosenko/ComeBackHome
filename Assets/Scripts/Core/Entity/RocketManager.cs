using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using Zenject;

namespace CBH.Core.Entity
{
    public class RocketManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI stateText;
        [SerializeField] private RocketManager rocketManager;
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private BoxCollider[] rocketBoxColliders;

        private static Coroutine timer;

        private GameData _gameData;

        private static float _localTimer;
        private bool _timerWasStart;

        public static UnityAction TimerStarter;
        private UnityAction LoaderLevel;

        public RocketState currentRocketState;

        public enum RocketState
        {
            Live,
            Dead,
            Win,
        }

        [Inject]
        private void Construct(GameData gameData)
        {
            _gameData = gameData;
        }

        private void RocketLoadState()
        {
            switch (currentRocketState)
            {
                case RocketState.Win:
                    LoaderNextSceneHandler();
                    break;
                case RocketState.Dead:
                    RestarterNextLevelHandler();
                    break;
            }
        }

        private void Awake()
        {
            rocketManager = this;
            rocketBoxColliders = GetComponentsInChildren<BoxCollider>();
        }

        private void Start()
        {
            _localTimer = 4f;
            _timerWasStart = false;
            currentRocketState = RocketState.Live;
            LoaderLevel = RocketLoadState;
            TimerStarter = StartTimer;
        }


        private void LoaderNextSceneHandler()
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene == SceneManager.sceneCountInBuildSettings)
                nextScene = 0;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            _gameData.SaveGame(nextScene);
            SceneManager.LoadScene(nextScene);
        }

        private void RestarterNextLevelHandler()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void StartTimer()
        {
            timer = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (_localTimer > 1)
            {
                ShowTextCounting();
                yield return new WaitForEndOfFrame();
                _localTimer -= Time.deltaTime;
            }

            _localTimer = 3;
            LoaderLevel.Invoke();
            yield return null;
        }

        private void ShowTextCounting()
        {
            string win, dead;
            win = "Посадка...";
            dead = "Неудача";
            if (_localTimer > 2f)
                stateText.text = $"{(_localTimer - 1):0}";
            else
            {
                switch (currentRocketState)
                {
                    case RocketState.Win:
                        stateText.text = win;
                        break;
                    case RocketState.Dead:
                        stateText.text = dead;
                        break;
                }
            }

            SetTextColor();
        }

        private void SetTextColor()
        {
            switch (currentRocketState)
            {
                case RocketState.Win:
                    stateText.color = Color.green;
                    break;
                case RocketState.Dead:
                    stateText.color = Color.red;
                    break;
            }
        }
    }
}