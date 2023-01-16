using UnityEngine;
using UnityEngine.Audio;

namespace CBH.Core.Audio
{
    public class AudioManager
    {
        private const string EnableMusicKey = "EnableMusic";
        private const string EnableSoundsKey = "EnableSounds";

        private const float EnableVolume = 0f;
        private const float DisableVolume = -80f;

        private AudioMixer _audioMixer;
        private bool _enableMusic;
        private bool _enableSounds;

        public bool EnableMusic
        {
            get => _enableMusic;
            set
            {
                _enableMusic = value;
                PlayerPrefs.SetInt(EnableMusicKey, _enableMusic ? 1 : 0);
                _audioMixer.SetFloat(EnableMusicKey, _enableMusic ? EnableVolume : DisableVolume);
            }
        }

        public bool EnableSounds
        {
            get => _enableSounds;
            set
            {
                _enableSounds = value;
                PlayerPrefs.SetInt(EnableSoundsKey, _enableSounds ? 1 : 0);
                _audioMixer.SetFloat(EnableSoundsKey, _enableSounds ? EnableVolume : DisableVolume);
            }
        }

        public AudioManager(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;

            _enableMusic = PlayerPrefs.GetInt(EnableMusicKey, 1) == 1;
            _enableSounds = PlayerPrefs.GetInt(EnableSoundsKey, 1) == 1;
        }

        public void Apply()
        {
            _audioMixer.SetFloat(EnableMusicKey, _enableMusic ? EnableVolume : DisableVolume);
            _audioMixer.SetFloat(EnableSoundsKey, _enableSounds ? EnableVolume : DisableVolume);
        }
    }
}