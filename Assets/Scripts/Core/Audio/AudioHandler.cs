using UnityEngine;
using Zenject;

namespace CBH.Core.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;

        [SerializeField] private AudioClip[] soundsAudioClips;

        private AudioManager _audioManager;

        private static AudioHandler audioHandler;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
        }

        public static void PlaySoundEffect(SoundEffect effect)
        {
            audioHandler.soundSource.PlayOneShot(audioHandler.soundsAudioClips[(int) effect]);
        }
    }
}