using UnityEngine;
using Zenject;

namespace CBH.Core.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;
        [SerializeField] private AudioSource loopedSoundsSource;

        [SerializeField] private AudioClip[] soundsAudioClips;

        private const int DelayForFixAudioMixerInit = 1000;

        private AudioManager _audioManager;

        private static AudioHandler audioHandler;
        
        private void Awake()
        {
            audioHandler = this;
        }

        [Inject]
        private void Inject(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        private void Start()
        {
            _audioManager.Apply();
            
            musicSource.Play(DelayForFixAudioMixerInit);
        }

        public static void PlaySoundEffect(SoundEffect effect)
        {
            audioHandler.soundSource.PlayOneShot(audioHandler.soundsAudioClips[(int) effect]);
        }

        public static void PlaySoundLooped(SoundEffect effect)
        {
            audioHandler.loopedSoundsSource.mute = false;
            audioHandler.loopedSoundsSource.clip = audioHandler.soundsAudioClips[(int) effect];
            audioHandler.loopedSoundsSource.Play();
        }

        public static void StopLoopSound()
        {
            audioHandler.loopedSoundsSource.mute = true;
        }
    }
}