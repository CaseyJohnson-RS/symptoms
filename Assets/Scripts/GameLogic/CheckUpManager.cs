using UnityEngine;          // Абсолютный каллокод.
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CheckUpManager : MonoBehaviour
{
    [Serializable]
    public struct CheckSet
    {
        public string name;
        public Button button;
        public bool requireCard;
    }

    public List<CheckSet> checkSets;
    public CardHand cards;
    public LogsCollector logsCollector;

    [Header("Events"), Space(5)]
    public UnityEvent<NPCData, string> onCheck;

    private NPCData npcData;

    // Mono events

    void Awake()
    {
        foreach(var checkSet in checkSets)
        {
            checkSet.button.onClick.AddListener(()=>
            {
                Check(checkSet.name);
            });
        }

    }

    void OnEnable()
    {
        CheckCards();
    }

    void Start()
    {
        ResetChecks();
    }

    // Internal API

    void ResetChecks()
    {
        foreach(var checkSet in checkSets)
            checkSet.button.interactable = true;
        
        CheckCards();
    }

    void Check(string name)
    {
        foreach(var checkSet in checkSets)
        {
            if (checkSet.name == name)
            {
                checkSet.button.interactable = false;
                if (checkSet.requireCard)
                {
                    if (ItemStorage.Instance.GetItem(ItemType.FILM).Count > 0)
                        ItemStorage.Instance.RemoveItem(ItemType.FILM);
                    else
                        return;
                }
                onCheck.Invoke(npcData, name);
                return;
            }
        }
    }

    void CheckCards()
    {
        int cardAmount = ItemStorage.Instance.GetItem(ItemType.FILM).Count;
        cards.SetCardCount(cardAmount);
        foreach(var checkSet in checkSets)
        {
            if (checkSet.requireCard && checkSet.button.interactable)
                checkSet.button.interactable = cardAmount > 0;
        }
    }

    // External API (Interface)

    public void SetNPC(NPCData data)
    {
        npcData = data;
        ResetChecks();
        logsCollector.Clear();
    }
}
