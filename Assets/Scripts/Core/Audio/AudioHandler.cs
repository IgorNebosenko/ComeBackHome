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
        [SerializeField] private AudioClip[] musicAudioClips;

        private const int DelayForFixAudioMixerInit = 1000;

        private AudioManager _audioManager;

        private MusicClip _currentMusicClip;

        private static AudioHandler audioHandler;
        
        private void Awake()
        {
            audioHandler = this;
            _currentMusicClip = MusicClip.MainTheme;
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

        public static void PlayMusicClip(MusicClip clip)
        {
            if (audioHandler._currentMusicClip == clip)
                return;

            audioHandler._currentMusicClip = clip;
            audioHandler.musicSource.clip = audioHandler.musicAudioClips[(int) clip];
            audioHandler.musicSource.Play();
        }
    }
}