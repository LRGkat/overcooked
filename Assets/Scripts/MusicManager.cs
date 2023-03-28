using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private AudioSource audioSource;
    public static MusicManager Instance { get; private set; }

    private float volume = .3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (Instance != null)Destroy(Instance);
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME,.3f);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
