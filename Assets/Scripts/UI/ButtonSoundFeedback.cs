using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class ButtonSoundFeedback : CustomButtonBase
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnPointerEnter(PointerEventData eventData) => PlayHover();
    public override void OnSelect(BaseEventData eventData) => PlayHover();


    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public override void OnPointerClick(PointerEventData eventData) => PlayClick();

    public override void OnSubmit(BaseEventData eventData) => PlayClick();

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
