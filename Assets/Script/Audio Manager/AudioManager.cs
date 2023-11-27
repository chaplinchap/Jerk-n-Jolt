using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")] 
    public AudioClip background;
    public AudioClip chargePush;
    public AudioClip airPush;
    public AudioClip pull;
    public AudioClip chargePull;
    public AudioClip airPull;
    public AudioClip dash;
    public AudioClip powerUP;
    public AudioClip powerUPRanOut;
    public AudioClip heartUP;
    public AudioClip respawn;
    public AudioClip countDown;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
   
}
