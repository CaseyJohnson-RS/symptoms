using UnityEngine;
using UnityEngine.Audio;

public class AudioClient : MonoBehaviour
{

    private AudioManager am;

    private void Start()
    {
        am = AudioManager.Instance;
    }

    public void PlayAudio(AudioData data)
    {
        am.Play(data);
    }
}
