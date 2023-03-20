using CBH.Core.Audio;
using CBH.Core.Entity;

namespace CBH.Core.Core.Entity.Visual
{
    public class SoundRocketEffects : IEntityVisual
    {
        private bool _lastState;

        public void Simulate(IInput input)
        {
            if (input.EnabledBoost == _lastState) 
                return;
            
            _lastState = !_lastState;
                
            if (_lastState)
                AudioHandler.PlaySoundLooped(SoundEffect.EngineBoost);
            else
                AudioHandler.StopLoopSound();
        }

        public void Stop()
        {
            AudioHandler.StopLoopSound();
        }
    }
}