using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounterSound : MonoBehaviour
{

    [SerializeField] private FurnaceCounter furnaceCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        furnaceCounter.OnStateChanged += FurnaceCounter_OnStateChanged;
        furnaceCounter.OnProgressChanged += FurnaceCounter_OnProgressChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }

    private void SoundManager_OnVolumeChanged(object sender, System.EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }

    private void FurnaceCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = furnaceCounter.IsBaked() && e.progressNormalized >= burnShowProgressAmount;


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

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 1f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound();
            }
        }
        
    }
}
