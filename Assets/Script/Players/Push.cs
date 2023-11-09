using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Push : MonoBehaviour
{
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;
    private PlayerMovement movement;

    //Push
    public float pushForce = 100;
    public KeyCode pushOnPress;
    private bool hasPressedPush = false;

    //Charge
    public float chargeTrackingTimer;
    public float minChargingTime = 2f;
    public bool ifFailedChargeTime;
    public bool ifSuccesChargeTime;
    public float maxChargeingTime = 4;
    private bool isChargingReal;
    private bool isStunned;
    public float speedReducerMultiplier = 0.75f;
    private float reducedSpeed;  //Initialiseret ved Start()
    private float originalSpeed; //Initialiseret ved Start()
    private float originalJump;  //Initialiseret ved Start()
    private float timeToUnstun;

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

        //Charge
        reducedSpeed = movement.speed * speedReducerMultiplier;
        originalSpeed = movement.speed;
        originalJump = movement.jumpingPower;
    }


    private void Update()
    {
        if (Input.GetKeyDown(pushOnPress))
        {
            hasPressedPush = true;
        }

        if (Input.GetKeyUp(pushOnPress)) 
        {
            hasPressedPush = false;
            audioManager.PlaySFX(audioManager.push);
            Invoke("ResetMaterial", flashTime);
        }

        if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            //puller.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

        Timer();
    }

    void ResetMaterial()
    {
        //puller.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void FixedUpdate()
    {
        ChargePush(1f, extraForce);
    }


        // METHODS //   

    private Vector3 VectorBetween()
    {
        Vector3 position;
        position = gameObject.GetComponent<Transform>().position;
        return (thePuller.transform.position - position);
    }

    private void ThePush(float extraForce) 
    {
        rigidbodyPuller.AddForce(VectorBetween().normalized * pushForce * extraForce, ForceMode2D.Impulse);
        hasPressedPush = false;
    }

    private void Pushing()
    {
        if (pushField.inField && hasPressedPush)
        {
            ThePush(1);
        }
    }


    public void ChargePush(float normalPull, float chargedPull)
    {

        if (isChargingReal && !isStunned)
        {
            movement.speed = reducedSpeed;
        }
        if (isChargingReal == false)
        {
            movement.speed = originalSpeed;
        }
        if (isStunned)
        {
            movement.speed = 0;
            movement.jumpingPower = 0;
            timeToUnstun += Time.deltaTime;
            if (timeToUnstun > 2f)
            {
                isStunned = false;
            }

        }
        if (!isStunned && !isChargingReal)
        {
            movement.speed = originalSpeed;
            movement.jumpingPower = originalJump;
        }

        if (ifFailedChargeTime && pushField.inField)
        {
            ThePush(normalPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

        if (ifSuccesChargeTime && pushField.inField)
        {
            ThePush(chargedPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

    }

    private void Timer()
    {

        if (Input.GetKeyDown(pushOnPress))
        {
            timeToUnstun = 0; 
            chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        else if (chargeTrackingTimer > maxChargeingTime)
        {
            chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        else if (Input.GetKeyUp(pushOnPress) && chargeTrackingTimer > minChargingTime && pushField.inField)
        {
            ifSuccesChargeTime = true;
            slowMotion.DoSlowmotion();
            //freeze.Freeze();
            SetPitch();
        }
        else if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            ifFailedChargeTime = true;
        }

        if (Input.GetKey(pushOnPress))
        {
            chargeTrackingTimer += Time.deltaTime;

            if (chargeTrackingTimer > 0.5f)
            {
                isChargingReal = true;
            }

            if (chargeTrackingTimer > maxChargeingTime)
            {
                isStunned = true;
            }
        }

        else if (Input.GetKeyUp(pushOnPress))
        {
            isChargingReal = false;
        }

        if (Input.GetKeyUp(pushOnPress) && isStunned)
        {
            timeToUnstun += Time.deltaTime;
        }


    }

}
