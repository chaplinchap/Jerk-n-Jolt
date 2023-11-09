using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Pull : MonoBehaviour
{
    private GameObject thePusher;
    private Rigidbody2D rigidbodyPusher;
    private FieldTrigger pullField;
    private BoxCollider2D boxColliderPusher;
    private PlayerMovement movement;
 
    //Pull
    private float time = 0.15f;
    public float pullForce = 10;
    public KeyCode pullOnPress;
    private bool hasPressedPull = false;
    public float extraForce = 1f;
    public LayerMask pusherLayer;
    private int defaultLayer = 0;
    private int pushLayer = 8;

    //Charge
    public float chargeTrackingTimer;
    private bool ifFailedChargeTime;
    private bool ifSuccesChargeTime;
    public float minChargingTime = 2f;
    public float maxChargeingTime = 4;
    private bool isChargingReal;
    private bool isStunned;
    public float speedReducerMultiplier = 0.75f;
    private float reducedSpeed;  //Initialiseret ved Start()
    private float originalSpeed; //Initialiseret ved Start()
    private float originalJump;  //Initialiseret ved Start()
    private float timeToUnstun;

    //SlowMotion
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
    public GameObject pusher;
    public float flashTime = 0.075f;
   


    public void SetPitch()
    {
        audioMixer.SetFloat("ExposedPitch", pitchValue);
        Debug.Log("Pitch Value: " + pitchValue);
        timeBox = Time.time;
    }


    void Start()
    {
        thePusher = GameObject.FindWithTag("Pusher");
        rigidbodyPusher = thePusher.GetComponent<Rigidbody2D>();
        pullField = gameObject.GetComponentInChildren<FieldTrigger>();
        boxColliderPusher = thePusher.GetComponent<BoxCollider2D>();
        chargeTrackingTimer = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        movement = GetComponent<PlayerMovement>();

        //Charge
        reducedSpeed = movement.speed * speedReducerMultiplier;
        originalSpeed = movement.speed;
        originalJump = movement.jumpingPower;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pullOnPress))
        {
            hasPressedPull = true;
        }

        if (Input.GetKeyUp(pullOnPress))
        {
            hasPressedPull = false;
            audioManager.PlaySFX(audioManager.pull);
            Invoke("ResetMaterial", flashTime);
        }

        if (Input.GetKeyUp(pullOnPress) && pullField.inField)
        {
            pusher.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

        Timer();

        
    }

    void ResetMaterial()
    {
        pusher.GetComponent<SpriteRenderer>().enabled = true;
    }


    private void FixedUpdate()
    {
        ChargePulling(1f , extraForce);
    }

        // METHODS //

    IEnumerator ChangeLayer()
    {   
        yield return new WaitForSeconds(time);
        boxColliderPusher.gameObject.layer = defaultLayer;
    }

    public Vector3 VectorBetween() 
    {
        Vector3 position;

        position = gameObject.GetComponent<Transform>().position;
        return (thePusher.transform.position - position);
    }

    private void ThePull(float extraForce) 
    {
        rigidbodyPusher.AddForce(-VectorBetween() * pullForce * extraForce, ForceMode2D.Impulse);
        hasPressedPull = false;
        boxColliderPusher.gameObject.layer = pushLayer;
        StartCoroutine(ChangeLayer());
    }

    private void Pulling()
    {

        if (pullField.inField && hasPressedPull == true)
        {
            ThePull(1);
        }
    }

    /*
    public void ChargePulling(float normalPull, float chargedPull)
    {

        if (ifFailedChargeTime && pullField.inField)
        {
            ThePull(normalPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

        if (ifSuccesChargeTime && pullField.inField)
        {
            ThePull(chargedPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        
    }
     */

    public void ChargePulling(float normalPull, float chargedPull)
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

        if (ifFailedChargeTime && pullField.inField)
        {
            ThePull(normalPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

        if (ifSuccesChargeTime && pullField.inField)
        {
            ThePull(chargedPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }

    }


    private void Timer()
    {

        if (Input.GetKeyDown(pullOnPress))
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

        else if (Input.GetKeyUp(pullOnPress) && chargeTrackingTimer > minChargingTime && pullField.inField)
        {
            ifSuccesChargeTime = true;
            slowMotion.DoSlowmotion();
            freeze.Freeze();
            SetPitch();
        }
        else if (Input.GetKeyUp(pullOnPress) && pullField.inField) 
        {
            ifFailedChargeTime = true;
        }


        if (Input.GetKey(pullOnPress))
        {
            chargeTrackingTimer += Time.deltaTime;

            if (chargeTrackingTimer > 0.5f)
            {
                isChargingReal = true;
            }

            if (chargeTrackingTimer > 4f)
            {
                isStunned = true;
            }
        }

        else if (Input.GetKeyUp(pullOnPress))
        {
            isChargingReal = false;
        }

        if (Input.GetKeyUp(pullOnPress) && isStunned)
        {
            timeToUnstun += Time.deltaTime;
        }


    }

}
