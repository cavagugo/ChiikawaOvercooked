using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class CustomButtonScale : CustomButtonBase
{
    private const float OriginalScale = 1.0f;
    [SerializeField] private float toScale = 1.1f;
    [SerializeField] private float duration = 0.15f;

    private bool _isReady = false;

    private void OnEnable()
    {
        _isReady = false;
        transform.DOKill();
        transform.localScale = Vector3.one * OriginalScale;
        StartCoroutine(MarkReady());
    }

    private void OnDisable()
    {
        _isReady = false;
        transform.DOKill();
        transform.localScale = Vector3.one * OriginalScale;
    }

    private IEnumerator MarkReady()
    {
        yield return null; // espera un frame
        _isReady = true;

        // Si este botón es el que está seleccionado al aparecer, animarlo
        if (EventSystem.current != null &&
            EventSystem.current.currentSelectedGameObject == gameObject)
        {
            HandleSelect();
        }
    }

    // --- SELECCIÓN ---
    public override void OnPointerEnter(PointerEventData eventData) => HandleSelect();
    public override void OnSelect(BaseEventData eventData) => HandleSelect();
    public override void OnPointerExit(PointerEventData eventData) => HandleDeselect();
    public override void OnDeselect(BaseEventData eventData) => HandleDeselect();

    private void HandleSelect()
    {
        if (!_isReady) return; // ignorar el Select prematuro de Show()
        transform.DOKill();
        transform.DOScale(toScale, duration)
                 .SetEase(Ease.InOutSine)
                 .SetUpdate(true); // funciona aunque Time.timeScale == 0
    }

    private void HandleDeselect()
    {
        transform.DOKill();
        transform.DOScale(OriginalScale, duration)
                 .SetEase(Ease.InOutSine)
                 .SetUpdate(true);
    }

    // --- CLICK ---
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        AnimateClick();
    }

    public override void OnSubmit(BaseEventData eventData) => AnimateClick();

    public void AnimateClick()
    {
        transform.DOKill();
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f)
                 .SetLoops(2, LoopType.Yoyo)
                 .SetUpdate(true)
                 .OnComplete(() => {
                     transform.DOScale(toScale, duration).SetUpdate(true);
                 });
    }
}