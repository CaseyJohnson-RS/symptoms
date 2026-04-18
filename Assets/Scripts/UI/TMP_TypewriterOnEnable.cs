using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TMP_TypewriterOnEnable : MonoBehaviour
{
    [Tooltip("Ссылка на TextMeshProUGUI / TextMeshPro на этом объекте")]
    public TMP_Text tmpText;

    [Tooltip("Время в секундах на один символ")]
    public float charsPerSecond = 20f;

    [Tooltip("Исходный полный текст, который будет «печататься»")]
    public string fullText = "Sample text";
    public UnityEvent onSingleType;

    private string currentText = "";
    private bool isTyping = false;

    private void OnEnable()
    {
        if (tmpText != null && !string.IsNullOrEmpty(fullText))
        {
            tmpText.text = "";
            currentText = "";
            isTyping = true;
            StartCoroutine(TypewriterRoutine());
        }
    }

    private void OnDisable()
    {
        isTyping = false;
        StopAllCoroutines();
        if (tmpText != null)
        {
            tmpText.text = "";
        }
    }

    private System.Collections.IEnumerator TypewriterRoutine()
    {
        if (charsPerSecond <= 0f) yield break;

        float delay = 1f / charsPerSecond;   // вот здесь было charsNormally

        foreach (char c in fullText)
        {
            if (!isTyping) yield break;

            currentText += c;
            tmpText.text = currentText;
            onSingleType.Invoke();

            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
    }
}