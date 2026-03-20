using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTimerSound : MonoBehaviour
{

    public static KitchenTimerSound Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void SoundManager_OnVolumeChanged(object sender, System.EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }



    public void PlayKitchenTimerSound()
    {
        audioSource.Play();
    }




    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        float timeLeftToRing = 10f;
        if (GameManager.Instance.GetRemainingTime() <= timeLeftToRing)
        {
            audioSource.UnPause();
        }
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        float timeLeftToRing = 10f;
        if (GameManager.Instance.GetRemainingTime() <= timeLeftToRing)
        {
            audioSource.Pause();
        }
    }
}
