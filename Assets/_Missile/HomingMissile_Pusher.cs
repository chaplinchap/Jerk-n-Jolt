using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile_Pusher : MonoBehaviour
{
    [SerializeField] private float damage = 1; 
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float missileSpeed = 20;
    [SerializeField] private float rotateSpeed = 200;
    [SerializeField] private GameObject DeathCircle;
    [SerializeField]private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (rb != null ) // If the original target is there
        target = GameObject.Find("Player1 Push").transform;

        else if (rb != null) // if the original target is not there
        {
            target = GameObject.Find("Player2 Pull").transform;
        }
        else // If nobody is on scene 
        {
            Debug.LogError("No players found!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // This will snap the missile to lock on target in a unatural way
        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position,missileSpeed*Time.deltaTime); // Missile will move towards target
        //transform.up = target.transform.position - transform.position; // Missile will rotate to look at target
      
        

        if (rb != null) // Only run if rb is active
        {
            // Calculate direction
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();

            // Calculate angle
            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            // Applied
            rb.angularVelocity = -rotateAmount * rotateSpeed; // Missile will rotate to look at target
            rb.velocity = transform.up * missileSpeed; // Missile will move forward
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("DeactivateMissileHoming")) // deactivate missile when getting near a player
        {
            rb = null;
        }
        if (collision.CompareTag("Pusher")) // On hit with Pusher
        {
            Debug.Log("hit pusher");
            audioManager.PlaySFX(audioManager.explosionSound);
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            Instantiate(DeathCircle, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 0f))); // Explosion on impact
            Destroy(gameObject); // Destroy missile
        }
        if (collision.CompareTag("Puller")) // On hit with Puller
        {
            Debug.Log("hit puller");
            audioManager.PlaySFX(audioManager.explosionSound);
            collision.GetComponent<HealthV2>().TakeDamage(damage); //The hit player takes damage
            Instantiate(DeathCircle, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 0f))); // Explosion on impact
            Destroy(gameObject); // Destroy missile
        }
    }
} 
