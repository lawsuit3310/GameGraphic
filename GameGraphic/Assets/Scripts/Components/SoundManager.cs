using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource aroowAudioSource;
    public AudioSource backgroundAudioSource;
    public AudioSource jumpAudioSource;

    public static SoundManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
        PlayBackgroundSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayArrowSound()
    {
        aroowAudioSource.Play();
    }

    public void PlayBackgroundSound()
    {
        backgroundAudioSource.Play();
    }
    public void PlayJumpSound()
    {
        jumpAudioSource.Play();
    }
}
