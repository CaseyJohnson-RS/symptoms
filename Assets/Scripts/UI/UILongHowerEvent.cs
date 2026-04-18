using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UILongHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Сколько секунд нужно держать курсор на объекте")]
    public float hoverHoldTime = 1.0f;

    [Tooltip("Вызывается, если курсор был на объекте дольше hoverHoldTime")]
    public UnityEvent onLongHover;

    private bool isHovering = false;
    private float hoverTimer = 0f;

    private void Update()
    {
        if (!isHovering)
            return;

        hoverTimer += Time.deltaTime;

        if (hoverTimer >= hoverHoldTime)
        {
            onLongHover?.Invoke();
            isHovering = false; // чтобы не вызывать событие много раз
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        hoverTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTimer = 0f;
    }
}