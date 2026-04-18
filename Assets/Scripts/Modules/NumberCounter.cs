using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumberCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private string format = "F0";

    public UnityEvent onCounterChange;
    public UnityEvent onEnd;

    private Coroutine _routine;
    private int _lastDisplayedNumber = -1;

    public void AnimateTo(float target)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(CountRoutine(target));
    }

    private IEnumerator CountRoutine(float target)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float eased = 1f - Mathf.Pow(1f - t, 3f);
            float currentValue = Mathf.Lerp(0f, target, eased);
            int displayedNumber = Mathf.RoundToInt(currentValue);

            // Вызываем событие только если число изменилось
            if (displayedNumber != _lastDisplayedNumber)
            {
                label.text = displayedNumber.ToString(format);
                onCounterChange.Invoke();
                _lastDisplayedNumber = displayedNumber;
            }

            yield return null;
        }

        label.text = target.ToString(format);
        _lastDisplayedNumber = Mathf.RoundToInt(target);
        _routine = null;
        onEnd.Invoke();
    }
}