using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class Push : MonoBehaviour
{
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;
    public PlayerMovement movement;

    //Push
    public float pushForce = 100;
    public KeyCode pushOnPress;
    private bool hasPressedPush = false;

    //Charge
    private float timer;
    public float chargingTime = 2f;
    public bool isCharging;
    public bool hasCharged;
    public float extraForce = 1;
    
    private bool isChargingReal;
    private bool isStunned;
    public float speedReducerMultiplier = 0.75f;     
    private float reducedSpeed;  //Initialiseret ved Start()
    private float originalSpeed; //Initialiseret ved Start()
    private float originalJump;  //Initialiseret ved Start()
    private float timeToUnstun; 

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
        //Charge
        reducedSpeed = movement.speed * speedReducerMultiplier;
        originalSpeed = movement.speed;
        originalJump = movement.jumpingPower;
        
        thePuller = GameObject.FindWithTag("Puller");
        rigidbodyPuller = thePuller.GetComponent<Rigidbody2D>();
        pushField = gameObject.GetComponentInChildren<FieldTrigger>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            puller.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

        Timer();
    }

    void ResetMaterial()
    {
        puller.GetComponent<SpriteRenderer>().enabled = true;
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

    public void ChargePush(float pushNormal, float pushCharged) 
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

        if (hasCharged && pushField.inField)
        {
            ThePush(pushCharged);
            isCharging = false;
            hasCharged = false;
        }

        if (isCharging && pushField.inField)
        {
            ThePush(pushNormal);
            isCharging = false;
            hasCharged = false;
        }

    }

    private void Timer()
    {
        if (Input.GetKeyDown(pushOnPress))
        {
            timer = 0;
            timeToUnstun = 0;
            isCharging = false;
            hasCharged = false;

        }
        else if (Input.GetKeyUp(pushOnPress) && timer > chargingTime && pushField.inField)
        {
            hasCharged = true;
            slowMotion.DoSlowmotion();
            freeze.Freeze();
            SetPitch();
        }

        else if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            isCharging = true;
        }

        if (Input.GetKey(pushOnPress))
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                isChargingReal = true;
            }

            if (timer > 4f)
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


