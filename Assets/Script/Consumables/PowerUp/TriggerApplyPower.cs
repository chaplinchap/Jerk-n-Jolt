using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyPower : ConsumableParentObject
{

    public float time = 5f; // Duration of the power up.

    public ConsumableScriptableObject powerUp;
    


 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller"))

        {
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
