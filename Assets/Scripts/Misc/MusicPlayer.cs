using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;
    int i = 0;

    void Start()
    {
        i--;
        PlayNext();
    }

    public void PlayNext()
    {
        StartCoroutine(PlayNextCoroutine());
    }

    public IEnumerator PlayNextCoroutine()
    {
        i = (++i) % songs.Length;
        audioSource.PlayOneShot(songs[i]);
        yield return new WaitForSecondsRealtime(songs[i].length);
        PlayNext();
    }
}
