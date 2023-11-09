using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Push : MonoBehaviour
{
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;


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
        if (ifFailedChargeTime && pushField.inField)
        {
            ThePush(pushNormal);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

        if (ifSuccesChargeTime && pushField.inField)
        {
            ThePush(pushCharged);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
    }

    private void Timer()
    {

        if (Input.GetKeyDown(pushOnPress))
        {
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
            freeze.Freeze();
            SetPitch();
        }
        else if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            ifFailedChargeTime = true;
        }

        if (Input.GetKey(pushOnPress))
        {
            chargeTrackingTimer += Time.deltaTime;
        }

   
    }

}
