using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class ItemInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent onHoverEnter;
    [SerializeField] private UnityEvent onHoverExit;
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private float clickDelay = 0.25f;

    private Coroutine clickCoroutine;

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
        // Если уже идет подсчет задержки, отменяем старый
        if (clickCoroutine != null)
            StopCoroutine(clickCoroutine);

        clickCoroutine = StartCoroutine(DelayedClick());
    }

    private IEnumerator DelayedClick()
    {
        yield return new WaitForSeconds(clickDelay);
        onClick.Invoke();
    }
}