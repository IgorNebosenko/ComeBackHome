using CBH.Core.Entity;
using UnityEngine;

namespace CBH.Core.Core.Entity.Motors
{
    public class RocketMotor
    {
        private Rigidbody _physicModel;
        private float _trustPower;
        private Transform _rocketTransform;
        private float _rotationPerSecond;

        public RocketMotor(Rigidbody physicModel, float trustPower, float rotationPerSecond)
        {
            _physicModel = physicModel;
            _trustPower = trustPower;
            _rocketTransform = physicModel.transform;
            _rotationPerSecond = rotationPerSecond;
        }

        public void Simulate(float deltaTime, IInput input)
        {
            if (input.EnabledBoost)
                _physicModel.AddRelativeForce(Vector3.up * (_trustPower * deltaTime));

            if (input.RotationDirection != 0)
            {
                _physicModel.freezeRotation = true;
                _rocketTransform.Rotate(Vector3.forward * _rotationPerSecond * input.RotationDirection * deltaTime);
                _physicModel.freezeRotation = false;
            }
        }
    }
}