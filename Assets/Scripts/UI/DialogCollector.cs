using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogCollector : MonoBehaviour
{
    [SerializeField] private GameObject NPCMessagePrefab; 
    [SerializeField] private GameObject PlayerMessagePrefab;
    [SerializeField, Range(0.01f, 1f)] private float typingDelay = 0.01f;
    [SerializeField] private RectTransform contentParent;

    private List<TextMeshProUGUI> chatMessages;

    private void Start()
    {
        chatMessages = new List<TextMeshProUGUI>();
    }

    private float GetTextTypingDuration(string text) { return typingDelay * text.Length; }
    private float GetMessagesTypingDuration(List<TextDelta> messages)
    {
        float duration = 0f;

        foreach(var message in messages)
        {
            duration += GetTextTypingDuration(message.text);
            duration += message.delay;
        }

        return duration;
    }

    // Coroutines

    private IEnumerator TypeText(TextMeshProUGUI target, string text)
    {
        target.text = text;
        target.ForceMeshUpdate();
        target.maxVisibleCharacters = 0;

        int totalCharacters = target.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            target.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    private IEnumerator TypeText(List<TextDelta> messages)
    {
        foreach(var message in messages)
        {
            TextMeshProUGUI obj = Instantiate(NPCMessagePrefab, contentParent).GetComponent<TextMeshProUGUI>();
            yield return StartCoroutine(TypeText(obj, message.text));
            chatMessages.Add(obj);

            yield return new WaitForSeconds(message.delay);
        }
    }

    // Interface

    public void Clear()
    {
        StopAllCoroutines();
        foreach(var message in chatMessages)
            Destroy(message);
        chatMessages.Clear();
    }

    // Returns duration of typing messages
    public float SendNPCMessages(List<TextDelta> messages)
    {
        StartCoroutine(TypeText(messages));
        return GetMessagesTypingDuration(messages);
    }

    // Returns duration of typing message
    public float SendPlayerMessage(string text)
    {
        TextMeshProUGUI message = Instantiate(PlayerMessagePrefab, contentParent).GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeText(message, text));
        chatMessages.Add(message);

        return GetTextTypingDuration(text);
    }
}
