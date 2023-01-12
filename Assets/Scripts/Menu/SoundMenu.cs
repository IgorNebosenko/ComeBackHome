using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    private MusicManagerHandler hander;
    public Toggle switcher;

    private void OnEnable()
    {
        hander = MusicManagerHandler.musicManager;
    }

    public void Start()
    {
        var check = hander.musicSource.enabled;
        switcher.isOn = check;
    }

    public void OnOffMusic()
    {
        hander.musicSource.enabled = switcher.isOn;
    }
}
