using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyPower : ConsumableParentObject
{

    public float time = 5f; // Duration of the power up.  

    public ConsumableScriptableObject powerUp;
    private bool triggerOnce = false;
    

    private float timeStampOnAwake;
    private float timeToDespawn = 8f;
    private float timeCoroutineDespawn = 0f;
    private bool isDespawned = false;
    AudioManager audioManager;



    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Awake()
    {
        timeStampOnAwake = Time.time;
        
    }

    private void Update()
    {
        if(Time.time - timeStampOnAwake  > timeToDespawn && !isDespawned && !triggerOnce) 
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
            TurnOffConsumable();
            powerUp.Apply(collision.gameObject);
            StartCoroutine(Durationbuff(powerUp, collision.gameObject, time));
            audioManager.PlaySFX(audioManager.powerUP);
            
        }

    }
    
}
