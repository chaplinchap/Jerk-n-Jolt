using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1; //Input how much damage to take
    public AudioClip[] pusherDeathSounds;
    public AudioClip[] pullerDeathSounds;
    private AudioSource audioSource;
    //private AudioManager audioManager;


    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        if (collision.CompareTag("Pusher"))
        {
            PusherDeathSounds();
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            CameraShake.Instance.ShakeCamera(CameraShakeValues.deathIntensity, CameraShakeValues.deathDuration);
        }
        if(collision.CompareTag("Puller")) //When one of the player is hit 
        {
            PullerDeathSounds();
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            CameraShake.Instance.ShakeCamera(CameraShakeValues.deathIntensity, CameraShakeValues.deathDuration);
        }  
    }

    void PusherDeathSounds()
    {
        AudioClip clip = pusherDeathSounds[Random.Range(0, pusherDeathSounds.Length)];
        audioSource.pitch = Random.Range(0.8f, 0.9f);
        audioSource.PlayOneShot(clip);
    }

    void PullerDeathSounds()
    {
        AudioClip clip = pullerDeathSounds[Random.Range(0, pullerDeathSounds.Length)];
        audioSource.pitch = Random.Range(1.1f, 1.3f);
        audioSource.PlayOneShot(clip);
    }

}           
            