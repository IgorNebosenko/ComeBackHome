using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MusicManagerHandler : MonoBehaviour
{
    [SerializeField] public static MusicManagerHandler musicManager;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] public AudioSource musicSource;


    void Awake()
    {
        
        if (musicManager == null)
        {
            musicManager = this;
            DontDestroyOnLoad(gameObject);
            musicSource = GetComponent<AudioSource>();
        }
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        
        if (musicClip != null && Time.timeScale == 1)
        {
            if (!musicSource.isPlaying)
            {
                musicSource.clip = musicClip;
                musicSource.Play();
            }
        }
    }

    public void ShutDown()
    {
        musicSource.gameObject.SetActive(!musicSource.gameObject.activeSelf);
    }

}
