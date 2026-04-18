using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct TextDelta
{
    [Multiline] public string text;
    [Range(0.1f, 2f)] public float delay;
}

[Serializable]
public struct QuestionAnswer
{
    [Multiline] public string question;
    public List<TextDelta> answers;
};

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    [Header("Common values"), Space(3)]
    [SerializeField] private string _name;
    [SerializeField] private bool male;
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

    [SerializeField, Tooltip("Слова, которые говорит персонаж при входе в комнату")]
    private List<TextDelta> entryLines;

    [Space(4)]

    [SerializeField, Tooltip("Диалог игрока с персонажем в виде простого вопрос-ответ")]
    private List<QuestionAnswer> dialog;

    [Space(4)]

    [SerializeField, Tooltip("Фразы, которые говорит NPC при разрешении пройти дальше")]
    private List<TextDelta> passingLines;

    [Space(4)]


    [SerializeField, Tooltip("Фразы, которые говорит NPC при запрете на прохождение дальше")]
    private List<TextDelta> stayingLines;


    // Interface

    public string Name => _name;
    public bool Male => male;

    public bool Infected => infected;

    public float Temperature => temperature;
    public float HeartRate => heartRate;

    public bool SymptomArms => arms;
    public bool SymptomMouth => mouth;
    public bool SymptomEyes => eyes;
    public bool SymptomArmpits => armpits;

    public Sprite Sprite => sprite;
    public AudioClip VoiceClip => voiceClip;

    public List<TextDelta> EntryLines => entryLines;

    public List<QuestionAnswer> Dialog => dialog;

    public List<TextDelta> PassingLines => passingLines;
    public List<TextDelta> StayingLines => stayingLines;
}
