using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButtonScale : CustomButtonBase
{
    private const float OriginalScale = 1.0f;
    [SerializeField] private float toScale = 1.1f;
    [SerializeField] private float duration = 0.15f;
    private Vector3 shrinkScale;

    private void Start()
    {
        shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    // --- LÓGICA DE SELECCIÓN (Mouse + Mando) ---

    public override void OnPointerEnter(PointerEventData eventData) => HandleSelect();
    public override void OnSelect(BaseEventData eventData) => HandleSelect();

    public override void OnPointerExit(PointerEventData eventData) => HandleDeselect();
    public override void OnDeselect(BaseEventData eventData) => HandleDeselect();

    private void HandleSelect()
    {
        transform.DOKill();
        transform.DOScale(toScale, duration).SetEase(Ease.InOutSine);
    }

    private void HandleDeselect()
    {
        transform.DOKill();
        transform.DOScale(OriginalScale, duration).SetEase(Ease.InOutSine);
    }

    // --- LÓGICA DE CLICK / PRESIONAR ---

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        AnimateClick();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        // OnSubmit es el equivalente al Click pero para mando/teclado
        AnimateClick();
    }

    public void AnimateClick()
    {
        transform.DOKill();
        // Usamos tu lógica de Yoyo
        transform.DOScale(shrinkScale, 0.1f)
                 .SetLoops(2, LoopType.Yoyo)
                 .OnComplete(() => {
                     // Al terminar, nos aseguramos de que vuelva a toScale si sigue seleccionado
                     transform.DOScale(toScale, duration);
                 });
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = Vector3.one * OriginalScale;
    }
}