using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Boombox : MonoBehaviour, IItem
{
    [SerializeField] private AudioData turnOnSound;
    [SerializeField] private AudioData turnOfSound;
    [SerializeField] private List<AudioData> playlist;

    private int current_track = -1;
    private bool isPlaying = false;

    private void PlayNext()
    {
        if (playlist.Count > 0)
        {
            current_track = (current_track + 1) % playlist.Count;
            AudioManager.Instance.Play(turnOnSound);
            AudioManager.Instance.Play(playlist[current_track]);
            isPlaying = true;
            
            StartCoroutine(Autoplay(playlist[current_track].ClipLength));
        }
    }

    public void Click()
    {
        if (isPlaying)
        {
            AudioManager.Instance.Play(turnOfSound);
            AudioManager.Instance.Stop(playlist[current_track].AudioGroup);
            isPlaying = false;
        }
        else
            PlayNext();
    }

    private IEnumerator Autoplay(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayNext();
    }

    private void OnDisable()
    {
        isPlaying = false;
        StopAllCoroutines();
    }
}
