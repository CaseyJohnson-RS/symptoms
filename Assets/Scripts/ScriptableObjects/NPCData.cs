using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct QuestionAnswer
{
    [Multiline] public string question;
    [Multiline] public string answer;
};

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    [Header("Common values"), Space(3)]
    [SerializeField] private string _name;
    [SerializeField] private bool infected;

    [Header("Symptoms"), Space(3)]
    [SerializeField, Range(-100f, 100f)] private float temperature;
    [SerializeField, Range(0f, 1000f)] private float heartRate;
    [SerializeField] private bool arms;
    [SerializeField] private bool mouth;
    [SerializeField] private bool eyes;
    [SerializeField] private bool armpits;

    [Header("Assets"), Space(3)]
    [SerializeField] private Sprite sprite;
    [SerializeField] private AudioClip voiceClip;

    [Header("Conversations"), Space(3)]
    [SerializeField] private List<QuestionAnswer> dialog;

    [Space(3)]
    [SerializeField] private string passingPhrase;
    [SerializeField] private string stayingPhrase;


    // Interface

    public string Name => _name;
    public bool Infected => infected;

    public float Temperature => temperature;
    public float HeartRate => heartRate;

    public bool SymptomArms => arms;
    public bool SymptomMouth => mouth;
    public bool SymptomEyes => eyes;
    public bool SymptomArmpits => armpits;

    public Sprite Sprite => sprite;
    public AudioClip VoiceClip => voiceClip;

    public List<QuestionAnswer> Dialog => dialog;

    public string PassingPhrase => passingPhrase;
    public string StayingPhrase => stayingPhrase;
}
