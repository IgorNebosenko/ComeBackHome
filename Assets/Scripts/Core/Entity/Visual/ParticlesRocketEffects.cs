using CBH.Core.Entity;
using UnityEngine;

namespace CBH.Core.Core.Entity.Visual
{
    public class ParticlesRocketEffects : IEntityVisual
    {
        private ParticleSystem _particlesLeft;
        private ParticleSystem _particlesRight;
        private ParticleSystem _boostEffect;

        private bool _isLeftPlaying;
        private bool _isRightPlaying;
        private bool _isBoostEffectPlaying;

        public ParticlesRocketEffects(ParticleSystem particlesLeft, ParticleSystem particlesRight, 
            ParticleSystem boostEffect)
        {
            _particlesLeft = particlesLeft;
            _particlesRight = particlesRight;
            _boostEffect = boostEffect;
        }

        public void Simulate(IInput input)
        {
            switch (input.RotationDirection)
            {
                case 0f:
                    StopAll();
                    break;
                case > 0f:
                    AddRightEffect();
                    break;
                case < 0f:
                    AddLeftEffect();
                    break;
            }
            
            SetBoostEffect(input.EnabledBoost);
        }

        private void StopAll()
        {
            _particlesLeft.Stop();
            _particlesRight.Stop();
            _boostEffect.Stop();

            _isLeftPlaying = false;
            _isRightPlaying = false;
            _isBoostEffectPlaying = false;
        }

        private void AddLeftEffect()
        {
            if (!_isLeftPlaying)
            {
                _particlesLeft.Play();
                _isLeftPlaying = true;
            }

            if (_isRightPlaying)
            {
                _particlesRight.Stop();
                _isRightPlaying = false;
            }
        }
        
        private void AddRightEffect()
        {
            if (!_isRightPlaying)
            {
                _particlesRight.Play();
                _isRightPlaying = true;
            }

            if (_isLeftPlaying)
            {
                _particlesLeft.Stop();
                _isLeftPlaying = false;
            }
        }

        private void SetBoostEffect(bool state)
        {
            if (_isBoostEffectPlaying == state)
                return;

            _isBoostEffectPlaying = !_isBoostEffectPlaying;
            
            if (_isBoostEffectPlaying)
                _boostEffect.Play();
            else
                _boostEffect.Pause();
        }
    }
}