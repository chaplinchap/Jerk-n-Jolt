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
    [SerializeField] private AudioSource stunSoundSource;
    [SerializeField] private AudioSource floorShakeSource;

    [Header("Audio Clip")] 
    public AudioClip[] background;
    public AudioClip chargePush;
    public AudioClip chargePull;
    public AudioClip powerUP;
    public AudioClip powerUPRanOut;
    public AudioClip heartUP;
    public AudioClip respawn;
    public AudioClip countDown;
    public AudioClip suddenDeathMusic;
    public AudioClip suddenDeathSound;
    public AudioClip floorShakeSound;
    public AudioClip explosionSound;
    public AudioClip consumableSpawn;
    public AudioClip stun;

    void Start()
    {
        AudioClip clip = background[UnityEngine.Random.Range(0, background.Length)];
        musicSource.PlayOneShot(clip);

        countDownSource.clip = countDown;
        countDownSource.Play();

        suddenDeathMusicSource.clip = suddenDeathMusic;

        stunSoundSource.clip = stun;

        floorShakeSource.clip = floorShakeSound;
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
    public void StartStunSound()
    {
        stunSoundSource.Play();
    }

    public void StopStunSound()
    {
        stunSoundSource.Stop();
    }
   

    public void StartFloorShake()
    {
        floorShakeSource.Play();
    }

    public void StopFloorShake()
    {
        floorShakeSource.Stop();
    }
}
