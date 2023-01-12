using UnityEngine;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private RocketManager _rocketManager;

    [SerializeField] private AudioClip deadthRocketSFX;
    [SerializeField] private AudioClip successSFX;

    [SerializeField] private ParticleSystem deadthRocketParticle;
    [SerializeField] private ParticleSystem successParticle;

    private AudioSource _audioSource;

    private bool isCalledTick;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rocketManager = GetComponent<RocketManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;

        if (_rocketManager.currentRocketState != RocketManager.RocketState.Dead)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    HandleFinishCollision(gameObject);
                    break;
                case "Obstacle":
                    HandleObstacleCollision(gameObject);
                    break;
            }
            if (!isCalledTick && collision.gameObject.tag != "Friendly")
            {
                isCalledTick = true;
                RocketManager.TimerStarter.Invoke();
            }
        }
    }
    private void HandleObstacleCollision(GameObject gameObject)
    {
            _rocketManager.currentRocketState = RocketManager.RocketState.Dead;
            _audioSource.Stop();
            _audioSource.PlayOneShot(deadthRocketSFX);
            deadthRocketParticle.Play();
            GetComponent<Movement>().enabled = false;
    }
    private void HandleFinishCollision(GameObject gameObject)
    {
        if (_rocketManager.currentRocketState != RocketManager.RocketState.Win)
        {
            _rocketManager.currentRocketState = RocketManager.RocketState.Win;
            _audioSource.Stop();
            _audioSource.PlayOneShot(successSFX);
            successParticle.Play();
            GetComponent<Movement>().enabled = false;
        }
    }
}
