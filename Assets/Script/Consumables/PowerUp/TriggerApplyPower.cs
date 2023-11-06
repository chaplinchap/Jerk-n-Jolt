using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyPower : ConsumableParentObject
{

    public float time = 5f; // Duration of the power up.  

    public ConsumableScriptableObject powerUp;
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
        if(Time.time - timeStampOnAwake  > timeToDespawn && !isDespawned) 
        {
            isDespawned = true;
            StartCoroutine(DespawnConsumable(timeCoroutineDespawn));
        
        }
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Pusher") && !triggerOnce || collision.CompareTag("Puller") && !triggerOnce)

        {
            triggerOnce = true;
            if(collision.CompareTag("Pusher"))
            {
                
                TurnOffConsumable();
                powerUp.ApplyPusher(collision.gameObject);
                StartCoroutine(DurationPusher(powerUp, collision.gameObject, time));
            }

            else if(collision.CompareTag("Puller"))
            {

                TurnOffConsumable();
                powerUp.ApplyPuller(collision.gameObject);
                StartCoroutine(DurationPuller(powerUp, collision.gameObject, time));
            }
        }
        
    }
}
