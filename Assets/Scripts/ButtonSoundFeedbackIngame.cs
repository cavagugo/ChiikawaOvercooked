using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundFeedbackIngame : CustomButtonBase
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnPointerEnter(PointerEventData eventData) => PlayHover();
    public override void OnSelect(BaseEventData eventData) => PlayHover();


    public void PlayHover()
    {
        SoundManager.Instance.PlayHover();
    }

    public override void OnPointerClick(PointerEventData eventData) => PlayClick();

    public override void OnSubmit(BaseEventData eventData) => PlayClick();

    public void PlayClick()
    {
        SoundManager.Instance.PlayClick();
    }
}
