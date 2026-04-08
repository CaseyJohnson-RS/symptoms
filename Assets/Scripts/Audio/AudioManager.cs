using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup[] audioGroups;

    private Dictionary<string, AudioSource> sources;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitSources();
    }

    public void Play(AudioData data)
    {
        if (!TryGetSource(data.AudioGroup, out var source)) return;

        source.clip = data.Clip;
        source.loop = data.IsLoop;

        if (data.IsShort)
            source.PlayOneShot(data.Clip);
        else
            source.Play();
    }

    public void Stop(AudioMixerGroup group)
    {
        if (TryGetSource(group, out var source))
            source.Stop();
    }

    public void StopAll()
    {
        foreach (var source in sources.Values)
            source.Stop();
    }

    public void SetVolume(AudioMixerGroup group, float linear)
    {
        linear = Mathf.Clamp01(linear);
        float dB = linear <= 0.0001f ? -80f : Mathf.Log10(linear) * 20f;
        audioMixer.SetFloat($"{group.name}Volume", dB);
    }

    private void InitSources()
    {
        sources = new Dictionary<string, AudioSource>();
        foreach (var group in audioGroups)
        {
            var go = new GameObject($"{group.name}_Source");
            go.transform.SetParent(transform);
            var source = go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = group;
            source.playOnAwake = false;
            sources[group.name] = source;
        }
    }

    private bool TryGetSource(AudioMixerGroup group, out AudioSource source)
    {
        return sources.TryGetValue(group.name, out source);
    }
}