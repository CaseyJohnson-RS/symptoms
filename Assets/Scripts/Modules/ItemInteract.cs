using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent onHoverEnter;
    [SerializeField] private UnityEvent onHoverExit;
    [SerializeField] private UnityEvent onClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}