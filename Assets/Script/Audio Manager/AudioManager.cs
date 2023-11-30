using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource countDownSource;
    [SerializeField] private AudioSource suddenDeathMusicSource;

    [Header("Audio Clip")] 
    public AudioClip background;
    public AudioClip chargePush;
    public AudioClip chargePull;
    public AudioClip powerUP;
    public AudioClip powerUPRanOut;
    public AudioClip heartUP;
    public AudioClip respawn;
    public AudioClip countDown;
    public AudioClip suddenDeathMusic;
    public AudioClip suddenDeathSound;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        countDownSource.clip = countDown;
        countDownSource.Play();

        suddenDeathMusicSource.clip = suddenDeathMusic;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopOldBackground()
    {
        musicSource.Stop();
    }

    public void StartSuddenDeathMusic()
    {
        suddenDeathMusicSource.PlayOneShot(suddenDeathMusic);
    }
   
}
