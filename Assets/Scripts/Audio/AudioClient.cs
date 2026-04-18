using UnityEngine;
using UnityEngine.Audio;

public class AudioClient : MonoBehaviour
{

    public AudioData playOnEnable;

    private AudioManager am;

    private void Start()
    {
        am = AudioManager.Instance;
    }

    private void OnEnable()
    {
        if (!am)
            am = AudioManager.Instance;

        if (playOnEnable)
            PlayAudio(playOnEnable);
    }

    public void PlayAudio(AudioData data)
    {
        am.Play(data);
    }
}
