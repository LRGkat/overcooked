using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;
    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryCounter.OnObjectDeliveried += DeliveryCounter_OnObjectDeliveried;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Instance_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void DeliveryCounter_OnObjectDeliveried(object sender, DeliveryCounter.OnObjectDeliveriedEventArgs e)
    {
        if (e.isSuccess)
        {
            PlaySound(audioClipRefsSO.deliveryFail, e.deliveryCounterTransform.position);
        }
        else
        {
            PlaySound(audioClipRefsSO.deliveryFail,e.deliveryCounterTransform.position);
        }
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop,baseCounter.transform.position);
    }

    private void Instance_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.pickUp,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position);
    }
    
    
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips[Random.Range(0,audioClips.Length)],position,volume);
    }
    private void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier= 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.footstep,position,volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
