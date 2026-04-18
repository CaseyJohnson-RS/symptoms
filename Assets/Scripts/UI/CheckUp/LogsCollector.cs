using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;


public class LogsCollector : MonoBehaviour
{
    [Serializable]
    public class CardView
    {
        public Image image;
        public GameObject root;
    }

    [SerializeField] private List<CardView> cards;
    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private TextMeshProUGUI heartRateText;

    [Header("Default values")]
    public string temperaturePrefix = "Температура ";
    public string heartRatePrefix = "Пульс ";
    public string noDataText = "[НЕТ ДАННЫХ]";
    

    public void Clear()
    {
        foreach(CardView card in cards)
            card.root.SetActive(false);
        
        temperatureText.text = temperaturePrefix + noDataText;
        heartRateText.text = heartRatePrefix + noDataText;
    }

    public void AddImage(Sprite sprite)
    {
        foreach (var card in cards)
        {
            if (card.root == null || card.image == null)
            {
                Debug.LogWarning("CardView has missing references!", this);
                continue;
            }

            if (!card.root.activeSelf)
            {
                card.image.sprite = sprite;
                card.root.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("No free card slots available!", this);
    }

    public void AddTemperature(float value)
    {
        temperatureText.text = temperaturePrefix + value.ToString("F1");
    }

    public void AddHeartRate(float value)
    {
        heartRateText.text = heartRatePrefix + value.ToString("F1");

    }
    
    
}
