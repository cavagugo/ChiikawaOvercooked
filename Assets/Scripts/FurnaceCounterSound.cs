using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounterSound : MonoBehaviour
{

    [SerializeField] private FurnaceCounter furnaceCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        furnaceCounter.OnStateChanged += FurnaceCounter_OnStateChanged;
    }

    private void FurnaceCounter_OnStateChanged(object sender, FurnaceCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == FurnaceCounter.State.Baking || e.state == FurnaceCounter.State.Baked;

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
