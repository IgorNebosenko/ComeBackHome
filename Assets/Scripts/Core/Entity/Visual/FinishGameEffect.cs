using CBH.Core.Audio;
using UnityEngine;

namespace CBH.Core.Core.Entity.Visual
{
    public class FinishGameEffect
    {
        private ParticleSystem _winEffect;
        private ParticleSystem _loseEffect;

        public FinishGameEffect(ParticleSystem winEffect, ParticleSystem loseEffect)
        {
            _winEffect = winEffect;
            _loseEffect = loseEffect;
        }

        public void PlayWin()
        {
            _winEffect.Play();
            AudioHandler.PlaySoundEffect(SoundEffect.Success);
        }

        public void PlayLose()
        {
            _loseEffect.Play();
            AudioHandler.PlaySoundEffect(SoundEffect.Death);
        }
    }
}