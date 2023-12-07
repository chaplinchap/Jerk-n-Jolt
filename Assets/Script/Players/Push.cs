using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Push : AbilityPower
{
    public ParticleSystem powerUPParticles;
    public ParticleSystem powerUPEndParticles;
    public ParticleSystem chargedUpParticles;
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;
    private PlayerMovement movement;
    private AbilityPower PullScript;
    private Stunner stun;

    private IEnumerator powerupParticle;
    
    //Push
    //public float abilityPower;
    //public KeyCode pushOnPress;
    private bool hasPressedPush = false;

    //Charge
    public float chargeTrackingTimer;
    public bool ifFailedChargeTime;
    public bool ifSuccesChargeTime;
    public float speedReducerMultiplier = 0.75f;


    public float extraForce = 1;
    

    //Audiosystem
    [Header("Audio")]
    private AudioManager audioManager;
    public AudioSource audioSourceChargedUp;
    public AudioSource audioSourcePushSounds;
    public AudioSource audioSourceAirPushSounds;
    public AudioClip[] pushSounds;
    public AudioClip[] airPushSounds;
    

    [SerializeField] private GameObject hitCircle;
    [SerializeField] private GameObject chargeCircle;
    

    void Start()
    {
        thePuller = GameObject.FindWithTag("Puller");
        rigidbodyPuller = thePuller.GetComponent<Rigidbody2D>();
        pushField = gameObject.GetComponentInChildren<FieldTrigger>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        PullScript = thePuller.GetComponent<AbilityPower>();
        
        movement = GetComponent<PlayerMovement>();
        
        var emission = chargedUpParticles.emission;
        emission.rateOverTime = 100f;

        stun = GetComponent<Stunner>();
    }


    private void Update()
    {
        hasPressedAbilityInGhostPusher = PressAbilityDown();

        if (Respawn.pusherIsDead)
        {
            StopCoroutine(powerupParticle);
        }

        if (upAbilityPress && !ifSuccesChargeTime && !pushField.inField)
        {
            AirPushSounds();
        }

        KeyInputs();
        Timer();
    }

    private void FixedUpdate()
    {
        
        ChargePush(1f, extraForce);  
       
       if (chargeTrackingTimer > minChargingTime)
       {
           if (Input.GetKey(abilityPress))
           {
               if (!audioSourceChargedUp.isPlaying)
               {
                   ChargeUpSound();
               }
               chargedUpParticles.Play();
               var emission = chargedUpParticles.emission;
               emission.rateOverTime = 100;
               StartCoroutine(ChargedUpParticles());
           } 
           if (Input.GetKeyUp(abilityPress) || stun.IsStunned())
           {
               audioSourceChargedUp.Stop();
               Invoke("ChargeTrackerTime", 0.1f);
               //chargeTrackingTimer = 0;
               StopCoroutine(ChargedUpParticles());
               var emission = chargedUpParticles.emission;
               emission.rateOverTime = 100f;
           }
       } 
       else 
       {
           var emission = chargedUpParticles.emission;
           emission.rateOverTime = 100f;
       }
    }
    
    IEnumerator ChargedUpParticles()
    {
        yield return new WaitForSeconds(1.3f);
        var emission = chargedUpParticles.emission;
        emission.rateOverTime = 300;
    }
        // METHODS //   
        
    private void ThePush(float extraForce) 
    {
        rigidbodyPuller.AddForce(VectorBetween(thePuller).normalized * abilityPowerForce * extraForce, ForceMode2D.Impulse);
        //hasPressedPush = false;
    }

    private void Pushing()
    {
        if (pushField.inField && hasPressedPush)
        {
            ThePush(1);
        }
    }


    public void ChargePush(float normalPush, float chargedPush)
    {
        if (ifFailedChargeTime && pushField.inField)
        {
            ThePush(normalPush);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
            StartCoroutine(PullScript.SetIsHit());
            PushSounds();

            Instantiate(hitCircle, thePuller.transform.position, Quaternion.Euler(0,0,Random.Range(0f, 360f)));
            CameraShake.Instance.ShakeCamera(CameraShakeValues.normalAbilityIntensity, CameraShakeValues.normalAbilityDuration);
        }
        else if (ifSuccesChargeTime && pushField.inField)
        {
            ThePush(chargedPush);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
            audioManager.PlaySFX(audioManager.chargePush);
            StartCoroutine(PullScript.SetIsHit());

            Instantiate(chargeCircle, thePuller.transform.position, Quaternion.identity);
            CameraShake.Instance.ShakeCamera(CameraShakeValues.chargedAbilityIntensity, CameraShakeValues.chargedAbilityDuration);
        }

    }
    private void Timer()
    {
        if (downAbilityPress)
        {
            Invoke("ChargeTrackerTime", 0.1f);
            //chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        
        else if (isAbilityPress)  
        {
            if (chargeTrackingTimer > maxChargeingTime)
            {
                Invoke("ChargeTrackerTime", 0.1f);
                //chargeTrackingTimer = 0;
                ifFailedChargeTime = false;
                ifSuccesChargeTime = false;
                return;
            }
            chargeTrackingTimer += Time.deltaTime;
            
        }
        else if (upAbilityPress && pushField.inField && chargeTrackingTimer > minChargingTime) 
        {
            ifSuccesChargeTime = true;
        }
        else if (upAbilityPress && pushField.inField)
        {
            ifFailedChargeTime = true;
        }
        else
        {
            Invoke("ChargeTrackerTime", 0.1f);
            //chargeTrackingTimer = 0;
        }
    }

    private void ChargeTrackerTime()
    {
        chargeTrackingTimer = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        powerupParticle = PowerUpParticles();

        if (collision.CompareTag("PowerUP"))
        {
            StartCoroutine(powerupParticle);
        }
    }

    public IEnumerator PowerUpParticles()
    {
        powerUPParticles.Play();
        yield return new WaitForSeconds(5f);
        powerUPEndParticles.Play();
        CameraShake.Instance.ShakeCamera(CameraShakeValues.powerUPEndIntensity, CameraShakeValues.powerUPEndDuration);
    }

    void ChargeUpSound()
    {
        audioSourceChargedUp.pitch = UnityEngine.Random.Range(1f, 1.5f);
        audioSourceChargedUp.Play();
    }

    void PushSounds()
    {
        AudioClip clip = pushSounds[UnityEngine.Random.Range(0, pushSounds.Length)];
        audioSourcePushSounds.PlayOneShot(clip);
    }

    void AirPushSounds()
    {
        AudioClip clip = airPushSounds[UnityEngine.Random.Range(0, airPushSounds.Length)];
        audioSourceAirPushSounds.pitch = Random.Range(0.8f, 0.9f);
        audioSourceAirPushSounds.volume = (2f);
        audioSourceAirPushSounds.PlayOneShot(clip);
    }
    
}
