using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerApplyHeart : ConsumableParentObject
{
    

    public ConsumableScriptableObject hearts;

    public float time = 3f;

    private bool triggerOnce = false;


    private float timeStampOnAwake;
    private float timeToDespawn = 11f;
    private float timeCoroutineDespawn = 0f;
    private bool isDespawned = false;
   

    private GameObject pusher;
    private GameObject puller;


    private IEnumerator buffDurationPusher;
    private IEnumerator buffDurationPuller;

    AudioManager audioManager;


    private void Awake()
    {
        timeStampOnAwake = Time.time;        

    }

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Time.time - timeStampOnAwake > timeToDespawn && !isDespawned && !triggerOnce)
        {
            isDespawned = true;
            StartCoroutine(DespawnConsumable(timeCoroutineDespawn));
        }


        if (Respawn.pullerIsDead)
        {
            StopCoroutine(buffDurationPuller);
            hearts.DeApply(puller);
            Destroy(this.gameObject);

        }

        if (Respawn.pusherIsDead)
        {
            StopCoroutine(buffDurationPusher);
            hearts.DeApply(pusher);
            Destroy(this.gameObject);
        }

        if (DeathGameChange.suddenDeathTriggered)
        {
            Destroy(this.gameObject);
        }



    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.CompareTag("Pusher") && !triggerOnce || collision.CompareTag("Puller") && !triggerOnce)

        {
            audioManager.PlaySFX(audioManager.heartUP);

            triggerOnce = true;
            buffDurationPusher = DurationbuffPusher(hearts, collision.gameObject, time);
            buffDurationPuller = DurationbuffPuller(hearts, collision.gameObject, time);
            TurnOffConsumable();

            if (collision.CompareTag("Pusher"))
            {
                pusher = collision.gameObject;                
                hearts.Apply(pusher);
                StartCoroutine(buffDurationPusher);
            }

            if (collision.CompareTag("Puller"))
            {
                puller = collision.gameObject;              
                hearts.Apply(puller);
                StartCoroutine(buffDurationPuller);
            }



            /*
            triggerOnce = true;
            
                TurnOffConsumable();
                hearts.Apply(collision.gameObject);                
                StartCoroutine(Durationbuff(hearts, collision.gameObject, time));
            */
        
        }



  


    }
}
