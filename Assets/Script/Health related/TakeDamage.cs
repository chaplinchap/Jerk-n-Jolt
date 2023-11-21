using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1; //Input how much damage to take
    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        Debug.Log("Hit damage!");
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller")) //When one of the player is hit 
        {
            audioManager.PlaySFX(audioManager.death);
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            CameraShake.Instance.ShakeCamera(CameraShakeValues.deathIntensity, CameraShakeValues.deathDuration);
        }   
    }       
}           
            