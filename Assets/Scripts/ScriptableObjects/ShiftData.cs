using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShiftData", menuName = "Scriptable Objects/ShiftData")]
public class ShiftData : ScriptableObject
{
    [SerializeField, Range(0f, 100f)] private float reward;
    [SerializeField, Range(0f, 200f)] private float penalty;
    [SerializeField, Space(5)] private List<NPCData> _NPCList;

    // Interface

    public List<NPCData> NPCList => _NPCList;
    public float Reward => reward;
    public float Penalty => penalty;
}
