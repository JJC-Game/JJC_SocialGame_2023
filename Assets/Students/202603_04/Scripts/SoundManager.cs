using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource[] seSource;

    public void SE_Play(int i)
    {
        seSource[i].Play();
    }

    public void BGM_Start()
    {
        bgmSource.Play();
    }

    public void BGM_Stop()
    {
        bgmSource.Stop();
    }
}
