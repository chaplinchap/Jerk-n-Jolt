using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : PowerUpParent // This class inherits from the class PowerUpParent which also inherits from MonoBehavior. It does this because it
                                       // calls the methods of Duration() and TurnOffContumable() used in the parent 
{
    public float time = 5f; // Duration of the power up.

    public PowerUpEffect speedUp; // it needs an child object of type PowerUpEffect, because PowerUpEffect is an abscract class.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") ^ collision.CompareTag("Puller")) // Check if the GameObject that trigger are either the Puller or Pusher
        {
            TurnOffConsumable(); // Turns off the Consumable
            speedUp.Apply(collision.gameObject); // Applies the powerup, in this specific case the speed power up
            StartCoroutine(Duration(speedUp, collision.gameObject, time)); // starts the coroutine Duration()
        }

    }
}
