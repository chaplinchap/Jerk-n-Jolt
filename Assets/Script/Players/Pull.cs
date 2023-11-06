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
    public float timer;
    private bool isCharging;
    private bool hasCharged;
    public float chargingTime = 2f;

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
        timer = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

    private Vector3 VectorBetween() 
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

    public void ChargePulling(float normalPull, float chargedPull)
    {

        if (isCharging && pullField.inField)
        {
            ThePull(normalPull);
            isCharging = false;
            hasCharged = false;
        }

        if (hasCharged && pullField.inField)
        {
            ThePull(chargedPull);
            isCharging = false;
            hasCharged = false;
        }
        
    }


    private void Timer()
    {

        if (Input.GetKeyDown(pullOnPress))
        {
            timer = 0;
            isCharging = false;
            hasCharged = false; 
        }
        else if (Input.GetKeyUp(pullOnPress) && timer > chargingTime && pullField.inField)
        {
            hasCharged = true;
            slowMotion.DoSlowmotion();
            freeze.Freeze();
            SetPitch();
        }
        else if (Input.GetKeyUp(pullOnPress) && pullField.inField) 
        {
            isCharging = true;
        }

        if (Input.GetKey(pullOnPress))
        {
            timer += Time.deltaTime;
        } 

        
    }

}
