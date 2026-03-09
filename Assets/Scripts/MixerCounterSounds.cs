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
