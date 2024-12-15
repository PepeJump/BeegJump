using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundSFX(AudioClip soundFX)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(soundFX);            
        }
    }

}
