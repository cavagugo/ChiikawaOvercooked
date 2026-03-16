using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipsRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFailed;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip furnaceFlick;
    public AudioClip mixerSpin;
    public AudioClip[] trash;
    public AudioClip warning;
    public AudioClip[] glaze;
    public AudioClip[] placeItemOnMixer;
    public AudioClip[] countdownSound;
}
