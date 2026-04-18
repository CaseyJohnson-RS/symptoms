using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHand : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject cardPrefab;

    [Header("UI")]
    public TextMeshProUGUI cardCountText;
    public string emptyText = "Нет карт";

    [Header("Fan Settings")]
    public float radius = 300f;
    public float angle = 40f;
    public float spacing = 120f;

    private readonly List<Transform> cards = new List<Transform>();

    // void Start()
    // {
    //     SetCardCount(0);
    // }

    void OnEnable()
    {
        Layout();
        UpdateCardCountText();
    }

    public void SetCardCount(int count)
    {
        if (cardPrefab == null)
        {
            Debug.LogError("CardHand: cardPrefab is not assigned");
            return;
        }

        while (cards.Count > count)
        {
            var t = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            if (t != null) Destroy(t.gameObject);
        }

        while (cards.Count < count)
        {
            var go = Instantiate(cardPrefab, transform);
            cards.Add(go.transform);
        }

        Layout();
        UpdateCardCountText();
    }

    void UpdateCardCountText()
    {
        if (cardCountText == null) return;

        if (cards.Count == 0)
            cardCountText.text = emptyText;
        else
            cardCountText.text = cards.Count.ToString();
    }

    public void Layout()
    {
        int count = cards.Count;
        if (count == 0) return;

        float step = count > 1 ? angle / (count - 1) : 0f;
        float startAngle = -angle / 2f;

        for (int i = 0; i < count; i++)
        {
            Transform card = cards[i];

            float a = startAngle + step * i;
            float rad = a * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(
                Mathf.Sin(rad) * spacing,
                Mathf.Cos(rad) * radius - radius,
                0f
            );

            Quaternion rot = Quaternion.Euler(0f, 0f, -a);

            card.localPosition = pos;
            card.localRotation = rot;
            card.localScale = Vector3.one;
        }
    }
}