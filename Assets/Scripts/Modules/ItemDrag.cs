using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private UnityEvent onDrag;
    [SerializeField] private UnityEvent onCancel;
    [SerializeField] private Vector2 objectSize;

    private RectTransform area;
    private RectTransform rectTransform;
    private Vector2 clickOffset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        area = transform.parent.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            area, eventData.position, eventData.pressEventCamera, out Vector2 mouseLocal);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            area, RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, rectTransform.position),
            eventData.pressEventCamera, out Vector2 objLocal);

        clickOffset = objLocal - mouseLocal;
        onDrag.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onCancel.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            area, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        localPoint += clickOffset;

        Rect areaRect = area.rect;
        Vector2 halfSize = objectSize * 0.5f;

        localPoint.x = Mathf.Clamp(localPoint.x, areaRect.xMin + halfSize.x, areaRect.xMax - halfSize.x);
        localPoint.y = Mathf.Clamp(localPoint.y, areaRect.yMin + halfSize.y, areaRect.yMax - halfSize.y);

        rectTransform.position = area.TransformPoint(localPoint);
    }
}