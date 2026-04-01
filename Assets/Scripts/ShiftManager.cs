using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ShiftManager : MonoBehaviour
{
    public ShiftData shiftData;
    public GameObject NPCPrefab;

    private NPC currentNPC;
    private int currentNPCDataIndex = 0;
    private List<bool> desicions;

    public UnityEvent onAllow;
    public UnityEvent onRefuse;
    public UnityEvent onShiftEnd;

    private bool shiftIsEnd = false;


    // public DialogManager dialogManager


    // Interface


    public void StartShift()
    {
        // Вызывает первого NPС и загружает диалог в DialogManager
        Next();
    }

    public void AllowPass()
    {
        // Анимирует уход и вызывает ручку отказа в Dialog Manager
        float duration = currentNPC.GoForward();
        Invoke("Next", duration);
        onAllow.Invoke();
    }

    public void RefusePass()
    {
        // Анимирует проход и вызывает ручку пропуска в Dialog Manager
        float duration = currentNPC.GoBack();
        Invoke("Next", duration);
        onRefuse.Invoke();
    }


    // Internal API
    

    private void Next()
    {
        if (shiftIsEnd)
            return;

        if (currentNPC)
            Destroy(currentNPC);

        if (currentNPCDataIndex >= shiftData.NPCList.Count)
        {
            EndShift();
            return;
        }
        
        currentNPC = NPC.CreateNPC(shiftData.NPCList[currentNPCDataIndex], NPCPrefab);
        currentNPC.Enter();
        
        ++currentNPCDataIndex;
    }

    private void EndShift()
    {
        // Show results screen
        shiftIsEnd = true;
        onShiftEnd.Invoke();
    }
}
