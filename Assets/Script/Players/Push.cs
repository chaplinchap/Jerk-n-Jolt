using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Push : AbilityPower
{
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;
    private PlayerMovement movement;
    private bool timeWait = false; //Used for GameStartCoolDown

    //Push
    //public float abilityPower;
    //public KeyCode pushOnPress;
    private bool hasPressedPush = false;

    //Charge
    public float chargeTrackingTimer;
    public float minChargingTime = 2f;
    public bool ifFailedChargeTime;
    public bool ifSuccesChargeTime;
    public float maxChargeingTime = 4;
    public float speedReducerMultiplier = 0.75f;


    public float extraForce = 1;

    // Slowmotion
    public SlowMotion slowMotion;

    // Freezer
    public Freezer freeze;

    // Audiosystem
    AudioManager audioManager;
    public AudioMixer audioMixer;
    public float pitchValue;
    private float timeBox;
    public float audioCoolDown;

    //Flash
    public GameObject puller;
    public float flashTime = 0.075f;



    public void SetPitch()
    {
        audioMixer.SetFloat("ExposedPitch", pitchValue);
        Debug.Log("Pitch Value: " + pitchValue);
        timeBox = Time.time;
    }

    void Start()
    {
        thePuller = GameObject.FindWithTag("Puller");
        rigidbodyPuller = thePuller.GetComponent<Rigidbody2D>();
        pushField = gameObject.GetComponentInChildren<FieldTrigger>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        movement = GetComponent<PlayerMovement>();
        abilityPowerForce = base.abilityPowerForce;
    }


    private void Update()
    {


        if (upAbilityPress) 
        {
            audioManager.PlaySFX(audioManager.push);
            //Invoke("ResetMaterial", flashTime);
        }

        /*
        if (Input.GetKeyDown(pushOnPress))
        {
            float timeBetween = Time.time - timeSinceLastPressed;


            if (timeBetween <= deadTimeBetweenPress)
            {
                hasPressedPush = false;

            }
            else {
                hasPressedPush = true;
            }

            timeSinceLastPressed = Time.time;
        }


        if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            //puller.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }
         */

        KeyInputs();

        Timer();
    }

    void ResetMaterial()
    {
        //puller.GetComponent<SpriteRenderer>().enabled = true;
    }

    
    private void FixedUpdate()
    {
        Invoke("waitForTime",3);
        if (timeWait)
        {
            ChargePush(1f, extraForce);
        }
        
    }
    private void waitForTime()
    {
        timeWait = true;
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
            CameraShake.Instance.ShakeCamera(CameraShakeValues.normalAbilityIntensity, CameraShakeValues.normalAbilityDuration);
        }

        if (ifSuccesChargeTime && pushField.inField)
        {
            ThePush(chargedPush);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;

            CameraShake.Instance.ShakeCamera(CameraShakeValues.chargedAbilityIntensity, CameraShakeValues.chargedAbilityDuration);
        }

    }

    private void Timer()
    {
        if (downAbilityPress)
        {
            chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        else if (isAbilityPress)
        {
            if (chargeTrackingTimer > maxChargeingTime)
            {
              
                chargeTrackingTimer = 0;
                ifFailedChargeTime = false;
                ifSuccesChargeTime = false;
                return;
            }

            chargeTrackingTimer += Time.deltaTime;
        }
        else if (upAbilityPress && chargeTrackingTimer > minChargingTime && pushField.inField)
        {
            ifSuccesChargeTime = true;
            slowMotion.DoSlowmotion();
            //freeze.Freeze();
            SetPitch();
        }
        else if (upAbilityPress && pushField.inField)
        {
            ifFailedChargeTime = true;
        }
        
    }

}
