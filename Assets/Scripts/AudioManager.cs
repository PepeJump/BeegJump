using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSourceSFX;
    public AudioSource audioSourceMusic;
    public AudioClip themeMusic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSourceMusic.clip = themeMusic;
        audioSourceMusic.Play();
    }

    public void PlaySoundSFX(AudioClip soundFX)
    {
        if (audioSourceSFX != null)
        {
            audioSourceSFX.PlayOneShot(soundFX);            
        }
    }

}
