using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup[] _audioMixerGroups;

    private Dictionary<string, AudioSource> _audioSources;

    public void PlayAudio(AudioData soundData)
    {
        AudioMixerGroup group = soundData.AudioGroup;
        AudioSource audioSource = _audioSources[group.name];
        audioSource.clip = soundData.Clip;
        audioSource.loop = soundData.IsLoop;
        if (soundData.IsShort)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            audioSource.Play();
        }
    }

    public void StopAudioGroup(AudioMixerGroup audioGroup)
    {
        _audioSources[audioGroup.name].Stop();
    }

    public void StopAllAudioGroups()
    {
        foreach (var audioSource in _audioSources.Values)
        {
            audioSource.Stop();
        }
    }

    public void PauseAudioGroup(AudioMixerGroup audioGroup, bool pause)
    {
        string groupName = audioGroup.name;
        if (pause)
        {
            _audioSources[groupName].Pause();
        }
        else
        {
            _audioSources[groupName].UnPause();
        }
    }

    /**
     * LinearVolume in range [0, 1]
     */
    public void SetAudioGroupVolume(AudioMixerGroup audioMixerGroup, float linearVolume)
    {
        if (linearVolume < 0 || linearVolume > 1)
        {
            throw new ArgumentException($"{linearVolume} must be between 0 and 1, inclusive.");
        }

        float dB = linearVolume <= 0.0001f ? -80f : Mathf.Log10(linearVolume) * 20f;
        _audioMixer.SetFloat($"{audioMixerGroup.name}Volume", dB);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        InitializeAudioGroups();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeAudioGroups()
    {
        _audioSources = new Dictionary<string, AudioSource>();
        foreach (AudioMixerGroup group in _audioMixerGroups)
        {
            string groupName = group.name;

            GameObject audioSourceObject = new GameObject($"{groupName}_AudioSource");
            audioSourceObject.transform.parent = transform;
            AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = group;
            audioSource.playOnAwake = false;
            audioSource.loop = false;

            _audioSources[groupName] = audioSource;
        }
    }

    private void OnDestroy()
    {
        DestroyAudioGroups();
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void DestroyAudioGroups()
    {
        if (_audioSources != null)
        {
            foreach (var source in _audioSources.Values)
            {
                if (source != null && source.gameObject != null)
                {
                    Destroy(source.gameObject);
                }
            }

            _audioSources.Clear();
        }
    }
}