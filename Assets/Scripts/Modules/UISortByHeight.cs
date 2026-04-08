using UnityEngine;
using System.Collections.Generic;

public class UISortByHeight : MonoBehaviour
{
    [SerializeField] private Transform parentToSort;

    private List<RectTransform> uiElements;

    void Awake()
    {
        uiElements = new List<RectTransform>();
        for (int i = 0; i < parentToSort.childCount; i++)
        {
            RectTransform rt = parentToSort.GetChild(i) as RectTransform;
            if (rt != null)
            {
                uiElements.Add(rt);
            }
        }
    }

    void LateUpdate()
    {
        uiElements.RemoveAll(rt => rt == null);
        if (uiElements.Count == 0) return;

        // Обратный порядок: чем БОЛЬШЕ position.y, тем выше в стеке
        uiElements.Sort((a, b) => b.position.y.CompareTo(a.position.y));

        for (int i = 0; i < uiElements.Count; i++)
        {
            uiElements[i].SetSiblingIndex(i);
        }
    }
}