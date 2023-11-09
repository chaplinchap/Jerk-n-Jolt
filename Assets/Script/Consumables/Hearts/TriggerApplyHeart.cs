using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerApplyHeart : ConsumableParentObject
{
    

    public ConsumableScriptableObject hearts;

    public float time = 5f;

    private bool triggerOnce = false;


    private float timeStampOnAwake;
    private float timeToDespawn = 5f;
    private float timeCoroutineDespawn = 3f;
    private bool isDespawned = false;

  

    private void Awake()
    {
        timeStampOnAwake = Time.time;
        

    }



    private void Update()
    {
        if (Time.time - timeStampOnAwake > timeToDespawn && !isDespawned)
        {
            isDespawned = true;
            StartCoroutine(DespawnConsumable(timeCoroutineDespawn));
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pusher") && !triggerOnce || collision.CompareTag("Puller") && !triggerOnce)

        {
           

            triggerOnce = true;
            if (collision.CompareTag("Pusher"))
            {
                TurnOffConsumable();
                hearts.ApplyPusher(collision.gameObject);                
                StartCoroutine(DurationPusher(hearts, collision.gameObject, time));
                
            }

            else if (collision.CompareTag("Puller"))
            {
                TurnOffConsumable();
                hearts.ApplyPuller(collision.gameObject);                
                StartCoroutine(DurationPuller(hearts, collision.gameObject, time));
                
            }
        }


    }
}
