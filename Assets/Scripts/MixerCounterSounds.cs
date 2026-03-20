using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerCounterSounds : MonoBehaviour
{
    [SerializeField] private MixerCounter mixerCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mixerCounter.OnStateChanged += MixerCounter_OnStateChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }
    private void SoundManager_OnVolumeChanged(object sender, System.EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }
    private void MixerCounter_OnStateChanged(object sender, MixerCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == MixerCounter.State.Mixing;

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

}
