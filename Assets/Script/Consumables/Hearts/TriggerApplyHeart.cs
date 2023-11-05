using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyHeart : ConsumableParentObject
{
    // Start is called before the first frame update

    public ConsumableScriptableObject hearts;

    public float time = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))

        {
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
