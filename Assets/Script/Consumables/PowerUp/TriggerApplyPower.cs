using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    private GameObject pusher;
    private GameObject puller;


    private IEnumerator buffDurationPusher;
    private IEnumerator buffDurationPuller;
    private IEnumerator powerUpParticlesPuller;
    private IEnumerator powerUpParticlesPusher;


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

        if (Respawn.pullerIsDead)
        {
            StopCoroutine(buffDurationPuller);
            StopCoroutine(powerUpParticlesPuller);
            powerUp.DeApply(puller);
            Destroy(this.gameObject);

        }

        if (Respawn.pusherIsDead)
        {
            StopCoroutine(buffDurationPusher);
            StopCoroutine(powerUpParticlesPusher);
            powerUp.DeApply(pusher);
            Destroy(this.gameObject);
        }


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.CompareTag("Pusher") && !triggerOnce || collision.CompareTag("Puller") && !triggerOnce)

        {
            audioManager.PlaySFX(audioManager.powerUP);

            triggerOnce = true;
            buffDurationPusher = DurationbuffPusher(powerUp, collision.gameObject, time);
            buffDurationPuller = DurationbuffPuller(powerUp, collision.gameObject, time);
            powerUpParticlesPusher = PowerUPRanOut();
            powerUpParticlesPuller = PowerUPRanOut();
            TurnOffConsumable();

            if(collision.CompareTag("Pusher"))
            {
                pusher = collision.gameObject;
                powerUp.Apply(pusher);
                StartCoroutine(buffDurationPusher);
                StartCoroutine(powerUpParticlesPusher);
            }

            if(collision.CompareTag("Puller"))
            {
                puller = collision.gameObject;
                powerUp.Apply(puller);
                StartCoroutine(buffDurationPuller);
                StartCoroutine(powerUpParticlesPuller);
            }


            /*
            powerUp.Apply(collision.gameObject);
            //StartCoroutine(Durationbuff(powerUp, collision.gameObject, time));
            StartCoroutine(buffDuration);
            */
        }

    }

    public IEnumerator PowerUPRanOut()
    {
        yield return new WaitForSeconds(time);
        audioManager.PlaySFX(audioManager.powerUPRanOut);
        CameraShake.Instance.ShakeCamera(CameraShakeValues.powerUPIntensity, CameraShakeValues.powerUPDuration);
    }


}
