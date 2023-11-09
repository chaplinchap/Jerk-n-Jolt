using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyChargeBuff : ConsumableParentObject
{ 


    public ConsumableScriptableObject charge;

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
                charge.ApplyPusher(collision.gameObject);
                StartCoroutine(DurationPusher(charge, collision.gameObject, time));

            }

            else if (collision.CompareTag("Puller"))
            {
                TurnOffConsumable();
                charge.ApplyPuller(collision.gameObject);
                StartCoroutine(DurationPuller(charge, collision.gameObject, time));

            }
        }


    }
}