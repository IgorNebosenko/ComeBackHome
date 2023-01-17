using UnityEngine;

namespace CBH.Core.Entity
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] RocketManager manager;
        [SerializeField] private AudioClip enginePowerSFX;

        [SerializeField] private ParticleSystem mainEngineParticle;
        [SerializeField] private ParticleSystem leftSideParticle;
        [SerializeField] private ParticleSystem rightSideParticle;

        private AudioSource _thrustSound;
        private Rigidbody _rigidBody;

        private float thrustPower;
        private float rotationAngle;

        bool MobileTouchThrust;
        int mobileDirection = 0;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _thrustSound = GetComponent<AudioSource>();
        }

        private void Start()
        {
            thrustPower = 5000f;
            rotationAngle = 200.0f;
        }

        private void FixedUpdate()
        {
            ProcessThrust();
            ProcessRotation();
        }

        public void IsThrustMobile(bool isT)
        {
            MobileTouchThrust = isT;
        }

        public void IsRotateMobile(int direction = 0)
        {
            mobileDirection = direction;
        }

        public void ProcessThrust()
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) || MobileTouchThrust)
                StartThrust();
            else
                StopThrust();
        }

        private void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A) || mobileDirection == -1)
                RotateLeft();
            else if (Input.GetKey(KeyCode.D) || mobileDirection == 1)
                RotateRight();
            else
                StopRotate();
        }

        public void StartThrust()
        {
            if (!_thrustSound.isPlaying)
                _thrustSound.PlayOneShot(enginePowerSFX);
            _rigidBody.AddRelativeForce(Vector3.up * (thrustPower * Time.deltaTime));
            mainEngineParticle.Play();
        }

        public void StopThrust()
        {
            mainEngineParticle.Stop();
            if (_thrustSound.isPlaying)
                _thrustSound.Stop();
        }

        private void RotateLeft()
        {
            ApplyRotation(rotationAngle);
            if (!rightSideParticle.isPlaying)
                rightSideParticle.Play();
        }

        private void RotateRight()
        {
            ApplyRotation(-rotationAngle);
            if (!leftSideParticle.isPlaying)
                leftSideParticle.Play();
        }

        private void StopRotate()
        {
            rightSideParticle.Stop();
            leftSideParticle.Stop();
        }

        private void ApplyRotation(float rotationThisFrame)
        {
            if (!(_rigidBody.constraints == RigidbodyConstraints.FreezeAll))
            {
                _rigidBody.freezeRotation = true;
                transform.Rotate(Vector3.forward * (rotationThisFrame + mobileDirection) * Time.deltaTime);
                _rigidBody.freezeRotation = false;
            }
        }
    }
}