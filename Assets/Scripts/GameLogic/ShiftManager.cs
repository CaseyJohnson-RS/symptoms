using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

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

    private int currentNPCDataIndex = 0;
    private List<bool> desicions;

    private bool shiftIsEnd = false;


    // public DialogManager dialogManager


    // Interface
    private void Start()
    {
        StartShift();
    }

    public void StartShift()
    {
        // Вызывает первого NPС и загружает диалог в DialogManager
        Next();
    }

    public void AllowPass()
    {
        // Анимирует уход и вызывает ручку отказа в Dialog Manager
       
        float duration = npc.Pass();
        dialogManager.Clear();
        Invoke("Next", duration);
        onAllow.Invoke();
    }

    public void RefusePass()
    {
        // Анимирует проход и вызывает ручку пропуска в Dialog Manager

        float duration = npc.Stay();
        dialogManager.Clear();
        Invoke("Next", duration);
        onRefuse.Invoke();
    }


    private void ShowDialog()
    {
        dialogManager.StartDialog();
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
        
        Invoke("ShowDialog", npc.Enter());
        
        ++currentNPCDataIndex;
    }

    private void EndShift()
    {
        // Show results screen
        shiftIsEnd = true;
        onShiftEnd.Invoke();
    }
}
