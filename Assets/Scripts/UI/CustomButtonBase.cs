using UnityEngine;
using UnityEngine.EventSystems;

// Añadimos ISelectHandler (foco), IDeselectHandler (pierde foco) e ISubmitHandler (botón A/Enter)
public abstract class CustomButtonBase : MonoBehaviour,
    IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler,
    ISelectHandler, IDeselectHandler, ISubmitHandler
{

    public virtual void OnPointerEnter(PointerEventData eventData) { }
    public virtual void OnPointerClick(PointerEventData eventData) { }
    public virtual void OnPointerExit(PointerEventData eventData) { }
    public virtual void OnPointerDown(PointerEventData eventData) { }

    // Métodos para Mando/Teclado
    public virtual void OnSelect(BaseEventData eventData) { }
    public virtual void OnDeselect(BaseEventData eventData) { }
    public virtual void OnSubmit(BaseEventData eventData) { }
}