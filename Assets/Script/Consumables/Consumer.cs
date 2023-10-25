using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : PowerUpParent
{

    public float time = 5f;

    public PowerUpEffect powerUpPush;
    public PowerUpEffect powerUpPull;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher")) {
            TurnOffConsumable();
            powerUpPush.Apply(collision.gameObject);
            StartCoroutine(Duration(powerUpPush, collision.gameObject, time));
        }

        if (collision.CompareTag("Puller")) 
        {
            TurnOffConsumable();
            powerUpPull.Apply(collision.gameObject);
            StartCoroutine(Duration(powerUpPull, collision.gameObject, time));
        
        }
    }

}
