using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerApplyPower : ConsumableParentObject
{

    public float time = 7f; // Duration of the power up.  

    public ConsumableScriptableObject powerUp;
    private bool triggerOnce = false;
    

    private float timeStampOnAwake;
    private float timeToDespawn = 11f;
    private float timeCoroutineDespawn = 0f;
    private bool isDespawned = false;

    private GameObject pusher;
    private GameObject puller;
    private ParticleSystem particleSystem;
    private Push push;
    private Pull pull;

    
    private IEnumerator buffDurationPusher;
    private IEnumerator buffDurationPuller;
    private IEnumerator powerUpParticlesPuller;
    private IEnumerator powerUpParticlesPusher;


    AudioManager audioManager;
    

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        particleSystem = GetComponent<ParticleSystem>();
        push = GameObject.FindGameObjectWithTag("Pusher").GetComponent<Push>();
        pull = GameObject.FindGameObjectWithTag("Puller").GetComponent<Pull>();
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

        try
        {
            if (Respawn.pullerIsDead || NegativeFeedbackLoopPuller.healthChangedPuller)
            {

                StopCoroutine(buffDurationPuller);
                StopCoroutine(powerUpParticlesPuller);
                powerUp.DeApply(puller);
                Destroy(this.gameObject);

            }
        }
        catch { }

        try
        {
            if (Respawn.pusherIsDead || NegativeFeedbackLoopPusher.healthChangedPusher)
            {
                StopCoroutine(buffDurationPusher);
                StopCoroutine(powerUpParticlesPusher);
                powerUp.DeApply(pusher);
                Destroy(this.gameObject);
            }
        }
        catch { }

        if (DeathGameChange.suddenDeathTriggered || UIManager.staticGameOver)
        {
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
            particleSystem.Stop();

            if(collision.CompareTag("Pusher"))
            {
                pusher = collision.gameObject;
                powerUp.Apply(pusher);
                StartCoroutine(buffDurationPusher);
                StartCoroutine(powerUpParticlesPusher);
                StartCoroutine(PusherParticles());
            }

            if(collision.CompareTag("Puller"))
            {
                puller = collision.gameObject;
                powerUp.Apply(puller);
                StartCoroutine(buffDurationPuller);
                StartCoroutine(powerUpParticlesPuller);
                StartCoroutine(PullerParticles());
            }
        }
    }

    public IEnumerator PowerUPRanOut()
    {
        yield return new WaitForSeconds(time);
        audioManager.PlaySFX(audioManager.powerUPRanOut);
        CameraShake.Instance.ShakeCamera(CameraShakeValues.powerUPIntensity, CameraShakeValues.powerUPDuration);
    }

    public IEnumerator PusherParticles()
    {
        push.powerUPParticles.Play();
        yield return new WaitForSeconds(time);
        push.powerUPParticles.Stop();
        push.powerUPEndParticles.Play();
    }

    public IEnumerator PullerParticles()
    {
        pull.powerUPParticles.Play();
        yield return new WaitForSeconds(time);
        pull.powerUPParticles.Stop();
        pull.powerUPEndParticles.Play();
    }

}
