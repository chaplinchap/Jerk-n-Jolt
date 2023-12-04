using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip mainMenuBackground;
    public AudioClip buttonClick;

    void Start()
    {
        musicSource.clip = mainMenuBackground;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    
}
