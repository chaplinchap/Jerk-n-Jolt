using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float timer;
    public float chargingTime = 2f;
    private bool isCharging;
    private bool hasCharged;

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
    private Material matFlash;
    private Material matDefault;
    SpriteRenderer flashRender;

    //Particles
    public ParticleSystem deathParticles;
    public ParticleSystem landingParticles;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        flashRender = GetComponent<SpriteRenderer>();
        matFlash = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = flashRender.material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pull")) 
        {
            //En eller form for koder der registrer om Pulleren har aktiveret hans pull-mechanic
            flashRender.material = matFlash;
            Invoke("ResetMaterial", 0.25f);
        }
        if (collision.CompareTag("DamageTrigger"))
        {
            DeathParticles();
        }
    }
   
    void ResetMaterial()
    {
        flashRender.material = matDefault;
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
            //freeze.Freeze();
        }
        Timer();

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

    }

    private void FixedUpdate()
    {
        ChargePush(1f * extraForce, 4f * extraForce);
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
        if (isCharging && pushField.inField)
        {
            ThePush(pushNormal);
            isCharging = false;
            hasCharged = false;
           
        }

        if (hasCharged && pushField.inField)
        {
            ThePush(pushCharged);
            isCharging = false;
            hasCharged = false;
   
        }
    }

    private void Timer()
    {

        if (Input.GetKeyDown(pushOnPress))
        {
            timer = 0;
            isCharging = false;
            hasCharged = false;
        }
        
        else if (Input.GetKeyUp(pushOnPress) && timer > chargingTime && pushField.inField)
        {
            hasCharged = true;
            //slowMotion.DoSlowmotion(); 
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
        }

    }

    public void setExtraForce(float force) 
    {
        extraForce = force;
    }

    void DeathParticles()
    {
        deathParticles.Play();
    }
}
