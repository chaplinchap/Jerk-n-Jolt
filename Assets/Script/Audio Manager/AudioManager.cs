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
    public AudioClip jump;
    public AudioClip push;
    public AudioClip chargePush;
    public AudioClip pull;
    public AudioClip hit;
    public AudioClip dash;
    public AudioClip charge;
    public AudioClip powerUP;
    public AudioClip powerUPRanOut;
    public AudioClip heartUP;
    public AudioClip death;
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
