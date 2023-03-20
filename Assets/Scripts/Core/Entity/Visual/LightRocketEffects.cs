using CBH.Core.Entity;
using UnityEngine;

namespace CBH.Core.Core.Entity.Visual
{
    public class LightRocketEffects : IEntityVisual
    {
        private Light _boostLight;
        private Light _leftRotationLight;
        private Light _rightRotationLight;
        
        private static readonly (float min, float max) RotationIntensityRange = (2f, 3f);
        private static readonly (float min, float max) BoostIntensityRange = (6f, 10f);

        public LightRocketEffects(Light boostLight, Light leftRotationLight, Light rightRotationLight)
        {
            _boostLight = boostLight;
            _leftRotationLight = leftRotationLight;
            _rightRotationLight = rightRotationLight;
        }

        public void Simulate(IInput input)
        {
            if (input.EnabledBoost)
                _boostLight.intensity = Random.Range(BoostIntensityRange.min, BoostIntensityRange.max);
            else
                _boostLight.intensity = 0f;

            switch (input.RotationDirection)
            {
                case > 0f:
                    _rightRotationLight.intensity = Random.Range(RotationIntensityRange.min, RotationIntensityRange.max);
                    _leftRotationLight.intensity = 0f;
                    break;
                case < 0f:
                    _leftRotationLight.intensity = Random.Range(RotationIntensityRange.min, RotationIntensityRange.max);
                    _rightRotationLight.intensity = 0f;
                    break;
                default:
                    _leftRotationLight.intensity = 0f;
                    _rightRotationLight.intensity = 0f;
                    break;
            }
        }

        public void Stop()
        {
            _boostLight.intensity = 0f;
            _leftRotationLight.intensity = 0f;
            _rightRotationLight.intensity = 0f;
            
        }
    }
}