using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhotoCardThrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform card;
    [SerializeField] private Image image;

    [Header("Animation")]
    [SerializeField] private float delay = 0f;
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private float startOffsetY = -1200f;
    [SerializeField] private float startRotation = 20f;
    [SerializeField] private float landRotation = -5f;

    [Header("Events")]
    public UnityEvent OnBeforeAnimation;
    public UnityEvent OnAfterAnimation;

    private Coroutine _routine;

    public void Throw(Sprite sprite)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(ThrowRoutine(sprite));
    }

    private IEnumerator ThrowRoutine(Sprite sprite)
    {
        OnBeforeAnimation?.Invoke();

        image.sprite = sprite;
        card.anchoredPosition = new Vector2(0f, startOffsetY);
        card.localRotation = Quaternion.Euler(0f, 0f, startRotation);

        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = 1f - Mathf.Pow(1f - t, 3f);

            card.anchoredPosition = Vector2.Lerp(new Vector2(0f, startOffsetY), Vector2.zero, eased);
            card.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(startRotation, landRotation, eased));

            yield return null;
        }

        card.anchoredPosition = Vector2.zero;
        card.localRotation = Quaternion.Euler(0f, 0f, landRotation);

        OnAfterAnimation?.Invoke();
        _routine = null;
    }
}