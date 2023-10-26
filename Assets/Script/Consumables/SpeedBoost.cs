using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUpParent
{

    public float time = 5f;

    public PowerUpEffect speedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") ^ collision.CompareTag("Puller"))
        {
            TurnOffConsumable();
            speedUp.Apply(collision.gameObject);
            StartCoroutine(Duration(speedUp, collision.gameObject, time));
        }

    }


}
