using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;

    [SerializeField] private Transform soundPosition;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f); //1f default 
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        GlazingCounter.OnAnyGlaze += GlazingCounter_OnAnyGlaze;
        MixerCounter.OnAnyIngredientAdded += MixerCounter_OnAnyIngredientAdded;
    }

    private void MixerCounter_OnAnyIngredientAdded(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.placeItemOnMixer, soundPosition.transform.position);
    }

    private void GlazingCounter_OnAnyGlaze(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.glaze, soundPosition.transform.position);    
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.trash, soundPosition.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        //float volume = 0.8f;
        PlaySound(audioClipsRefsSO.objectDrop, soundPosition.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        //float volume = 0.8f;
        PlaySound(audioClipsRefsSO.objectPickup, soundPosition.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        //float volume = 0.8f;
        PlaySound(audioClipsRefsSO.chop, soundPosition.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        //DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliveryFailed, soundPosition.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        //DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliverySuccess, soundPosition.transform.position);
    }

    public void PlayCountdownSound(int soundIndex)
    {
        PlaySound(audioClipsRefsSO.countdownSound[soundIndex], soundPosition.transform.position);
    }

    private void PlaySound (AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) //volumen default a 1f
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) //volumen default a 1f
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void ChangeVolume(float volume)
    {
        this.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
