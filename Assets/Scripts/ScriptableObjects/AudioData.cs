using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioMixerGroup audioGroup;
    [SerializeField] private bool isShort;
    [SerializeField] private bool isLoop;

    public AudioClip Clip => clip;
    public AudioMixerGroup AudioGroup => audioGroup;
    public bool IsShort => isShort;
    public bool IsLoop => isLoop;
    public float ClipLength => clip.length;
}