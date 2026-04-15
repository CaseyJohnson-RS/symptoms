using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionCollector : MonoBehaviour
{
    [SerializeField] private GameObject OptionPrefab;
    [SerializeField] private RectTransform contentParent;
    [SerializeField, Range(0.01f, 1f)] private float typingDelay = 0.01f;

    private List<Button> optionButtons;

    private void Start()
    {
        optionButtons = new List<Button>();
    }

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

    // Interface

    public void SetOptions(List<QuestionAnswer> qalist, Action<int> callback)
    {
        for(int i = 0; i < qalist.Count; ++i)
        {
            int index = i;
            GameObject go = Instantiate(OptionPrefab, contentParent);
            var btnComp =  go.GetComponent<Button>();

            btnComp.onClick.AddListener(() => callback(index));
            optionButtons.Add(btnComp);

            StartCoroutine(TypeText(go.GetComponent<TextMeshProUGUI>(), qalist[i].question));
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
        foreach(var btn in optionButtons)
            Destroy(btn.gameObject);
        optionButtons.Clear();
    }

    public void Deactivate()
    {
        foreach(var btn in optionButtons)
            btn.interactable = false;
    }
    
    public void Activate()
    {   
        foreach(var btn in optionButtons)
            btn.interactable = true;
    }
}
