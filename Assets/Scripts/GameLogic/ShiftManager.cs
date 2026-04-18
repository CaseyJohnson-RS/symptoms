using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShiftManager : MonoBehaviour
{
    [Header("Shift data")]
    [Space(4)] public ShiftData shiftData;

    [SerializeField] private NPC npc;

    [Tooltip("Событие срабатывающее после прохождения/остановки первого персонажа в списке")]
    [Space(4)] public UnityEvent onFirstNPCLeft;

    [Header("Common events")]

    [Space(4)] public UnityEvent onAllow;
    [Space(4)] public UnityEvent onRefuse;
    [Space(4)] public UnityEvent onShiftEnd;

    [Header("Managers"), Space(5)]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private CheckUpManager checkUpManager;

    [Header("Buttons")]
    [SerializeField] private Button allowPass;
    [SerializeField] private Button refusePass;
    [SerializeField] private Button checkUp;

    [Header("Scores")]
    [SerializeField] private TextMeshProUGUI totalPeople;
    [SerializeField] private TextMeshProUGUI healthIn;
    [SerializeField] private TextMeshProUGUI illIn;
    [SerializeField] private TextMeshProUGUI totalMoney;


    private int currentNPCDataIndex = 0;
    private List<bool> desicions;

    private bool shiftIsEnd = false;


    // public DialogManager dialogManager


    private void SetControllsInteract(bool interactable)
    {
        allowPass.interactable = interactable;
        refusePass.interactable = interactable;
        checkUp.interactable = interactable;
    }

    private void EnableControlls() => SetControllsInteract(true);
    

    // Interface
    private void Start()
    {
        StartShift();
        desicions = new List<bool>();
    }

    public void StartShift()
    {
        // Вызывает первого NPС и загружает диалог в DialogManager
        Next();
    }

    public void AllowPass()
    {
        // Анимирует уход и вызывает ручку отказа в Dialog Manager
        SetControllsInteract(false);
        desicions.Add(true);
        float duration = npc.Pass();
        dialogManager.Clear();
        Invoke("Next", duration);
        onAllow.Invoke();
    }

    public void RefusePass()
    {
        // Анимирует проход и вызывает ручку пропуска в Dialog Manager
        SetControllsInteract(false);
        desicions.Add(false);
        float duration = npc.Stay();
        dialogManager.Clear();
        Invoke("Next", duration);
        onRefuse.Invoke();
    }


    private void ShowDialog()
    {
        dialogManager.StartDialog();
        Invoke("EnableControlls", 3f);
    }

    // Internal API
    
    private void Next()
    {
        if (shiftIsEnd)
            return;

        if (currentNPCDataIndex >= shiftData.NPCList.Count)
        {
            EndShift();
            return;
        }

         if (currentNPCDataIndex == 1)
            onFirstNPCLeft.Invoke();
        
        // Setup
        npc.UpdateNPC(shiftData.NPCList[currentNPCDataIndex]);
        dialogManager.SetDialog(
            shiftData.NPCList[currentNPCDataIndex].Dialog,
            shiftData.NPCList[currentNPCDataIndex].EntryLines,
            shiftData.NPCList[currentNPCDataIndex].PassingLines,
            shiftData.NPCList[currentNPCDataIndex].StayingLines
        );
        
        npc.Enter();
        checkUpManager.SetNPC(shiftData.NPCList[currentNPCDataIndex]);


        Invoke("ShowDialog", 0.1f);
        
        ++currentNPCDataIndex;
    }

    private void EndShift()
    {
        // Show results screen
        shiftIsEnd = true;

        totalPeople.text = shiftData.NPCList.Count.ToString();

        int health = 0;
        int ill = 0;

        for(int i = 0; i < shiftData.NPCList.Count; ++i)
        {
            if (desicions[i])
            {
                if (shiftData.NPCList[i].Infected)
                    ++ill;
                else
                    ++health;
            }
        }

        healthIn.text = health.ToString();
        illIn.text = ill.ToString();
        totalMoney.text = (health * shiftData.Reward + ill * shiftData.Penalty).ToString();

        onShiftEnd.Invoke();
    }
}
