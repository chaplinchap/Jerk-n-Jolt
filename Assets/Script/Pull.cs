using System.Collections;
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
        thePusher = GameObject.FindWithTag("Pusher");
        rigidbodyPusher = thePusher.GetComponent<Rigidbody2D>();
        pullField = gameObject.GetComponentInChildren<FieldTrigger>();
        boxColliderPusher = thePusher.GetComponent<BoxCollider2D>();
        timer = 0;

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
        if (Input.GetKeyDown(pullOnPress))
        {
            hasPressedPull = true;
            
        }

        if (Input.GetKeyUp(pullOnPress))
        {
            hasPressedPull = false;
            audioManager.PlaySFX(audioManager.pull);
            freeze.Freeze();
        }

        Timer();
        if (Time.time - timeBox > audioCoolDown)
        {
            audioMixer.SetFloat("ExposedPitch", 1f);
        }

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
        rigidbodyPusher.AddForce(-VectorBetween().normalized * pullForce * extraForce, ForceMode2D.Impulse);
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
    void DeathParticles()
    {
        deathParticles.Play();
    }

}
