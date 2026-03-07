using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerCounterVisual : MonoBehaviour
{
    private const string OPEN = "Open";
    private const string MIXING = "Mixing";
    [SerializeField] private MixerCounter mixerCounter;
    [SerializeField] private GameObject ingredients;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        mixerCounter.OnStateChanged += MixerCounter_OnStateChanged;
        mixerCounter.OnIngredientAdded += MixerCounter_OnIngredientAdded;
        mixerCounter.OnDoughGrabbed += MixerCounter_OnDoughGrabbed;
        ingredients.SetActive(false);
    }

    private void MixerCounter_OnDoughGrabbed(object sender, System.EventArgs e)
    {
        ingredients.SetActive(false);
    }

    private void MixerCounter_OnIngredientAdded(object sender, MixerCounter.OnIngredientAddedEventArgs e)
    {
        if (!ingredients.activeSelf)
        {
            ingredients.SetActive(true);
        }
    }

    private void MixerCounter_OnStateChanged(object sender, MixerCounter.OnStateChangedEventArgs e)
    {
        if (e.state == MixerCounter.State.Mixed || e.state == MixerCounter.State.Idle)
        {
            animator.SetBool(OPEN, true);
            animator.SetBool(MIXING, false);
        }
        if (e.state == MixerCounter.State.Mixing)
        {
            animator.SetBool(OPEN, false);
            animator.SetBool(MIXING, true);
        }

    }

}
