using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1; //Input how much damage to take
    public AudioClip[] deathSounds;
    private AudioSource audioSource;
    //private AudioManager audioManager;


    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    int test;

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller")) //When one of the player is hit 
        {
            DeathSounds();
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            CameraShake.Instance.ShakeCamera(CameraShakeValues.deathIntensity, CameraShakeValues.deathDuration);
        }   
    }

    void DeathSounds()
    {
        AudioClip clip = deathSounds[Random.Range(0, deathSounds.Length)];
        audioSource.pitch = Random.Range(0.7f, 0.8f);
        audioSource.PlayOneShot(clip);
    }


}           
            