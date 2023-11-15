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
    private bool timeWait = false; //Used for GameStartCoolDown

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
    public float speedReducerMultiplier = 0.75f;


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
    }

    private void OnEnable()
    {
        if (Input.GetKey(pullOnPress)) {
            hasPressedPull = true;
            return;
        }

        hasPressedPull = false;
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
            //pusher.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

        Timer();

        
    }

    void ResetMaterial()
    {
        //pusher.GetComponent<SpriteRenderer>().enabled = true;
    }
    
    private void FixedUpdate()
    {
        Invoke("waitForTime",3);
        if (timeWait)
        {
            ChargePulling(1f , extraForce);
        }
        
    }
    private void waitForTime()
    {
        timeWait = true;
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

        if (pullField.inField && hasPressedPull)
        {
            ThePull(1);
        }
    }

    public void ChargePulling(float normalPull, float chargedPull)
    {
        if (ifFailedChargeTime && pullField.inField)
        {
            ThePull(normalPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;

            CameraShake.Instance.ShakeCamera(CameraShakeValues.normalAbilityIntensity, CameraShakeValues.normalAbilityDuration);
        }

        if (ifSuccesChargeTime && pullField.inField)
        {
            ThePull(chargedPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;

            CameraShake.Instance.ShakeCamera(CameraShakeValues.chargedAbilityIntensity, CameraShakeValues.chargedAbilityDuration);
        }
    }


    private void Timer()
    {
        if (Input.GetKeyDown(pullOnPress))
        {
            chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
        }
        else if (Input.GetKey(pullOnPress))
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
        else if (Input.GetKeyUp(pullOnPress) && chargeTrackingTimer > minChargingTime && pullField.inField)
        {
            ifSuccesChargeTime = true;
            slowMotion.DoSlowmotion();
            //freeze.Freeze();
            SetPitch();
        }
        else if (Input.GetKeyUp(pullOnPress) && pullField.inField)
        {
            ifFailedChargeTime = true;
        }

    }

    public bool GetHasPressedPull() { return hasPressedPull; }

}
