using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // public NPCData data;
    private List<QuestionAnswer> qa;
    private List<TextDelta> entryLines;
    private List<TextDelta> passingLines;
    private List<TextDelta> stayingLines;

    private CanvasGroup canvasGroup;

    public OptionCollector optionCollector;
    public DialogCollector dialogCollector;

    void Start()
    {
        // SetDialog(data.Dialog, data.EntryLines, data.PassingLines, data.StayingLines);
        // StartDialog();

        canvasGroup = GetComponent<CanvasGroup>();
    }


    private void ShowEntryLines()
    {
        float duration = dialogCollector.SendNPCMessages(entryLines);
        Invoke("SetOptions", duration + 1f);
    }

    private void SetOptions()
    {
        optionCollector.SetOptions(qa, Ask);
    }

    private IEnumerator SendNPCMessage(int option, float inSeconds)
    {
        yield return new WaitForSeconds(inSeconds);
        yield return new WaitForSeconds(dialogCollector.SendNPCMessages(qa[option].answers) + 0.2f);
        optionCollector.Activate();
    }

    // Interface

    public void Ask(int option)
    {
        optionCollector.Deactivate();
        float delay = dialogCollector.SendPlayerMessage(qa[option].question);
        StartCoroutine(SendNPCMessage(option, delay + 1f));
    }

    public void SetDialog(List<QuestionAnswer> dialog, List<TextDelta> _entryLines, List<TextDelta> _passingLines, List<TextDelta> _stayingLines)
    {
        qa = dialog;
        entryLines = _entryLines;
        passingLines = _passingLines;
        stayingLines = _stayingLines;
    }

    public void StartDialog()
    {
        StartCoroutine(FadeIn());
    }

    public float ShowPassingLines()
    {
        optionCollector.Deactivate();
        float duration = dialogCollector.SendNPCMessages(passingLines);
        Invoke("Clear", duration);
        return duration;
    }

    public float ShowStayingLines()
    {
        optionCollector.Deactivate();
        float duration = dialogCollector.SendNPCMessages(stayingLines);
        Invoke("Clear", duration);
        return duration;
    }

    public void Clear()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    // ---------------------------------------------------------------------

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeRoutine(0f, 0.1f));
        optionCollector.Clear();
        dialogCollector.Clear();
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(FadeRoutine(1f, 0.1f));
        Invoke("ShowEntryLines", 2f);
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;  // Точное значение
    }
}
