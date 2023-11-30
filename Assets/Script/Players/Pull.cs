using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class Pull : AbilityPower
{
    public ParticleSystem powerUPParticles;
    public ParticleSystem powerUPEndParticles;
    public ParticleSystem chargedUpParticles;
    private GameObject thePusher;
    private Rigidbody2D rigidbodyPusher;
    private FieldTrigger pullField;
    private BoxCollider2D boxColliderPusher;
    private PlayerMovement movement;
    private AbilityPower PushScript;
    private Stunner stun;

    //Pull
    private float time = 0.15f;

    //public float abilityPower;
    //public KeyCode pullOnPress;
    private bool hasPressedPull = false;
    public float extraForce = 1f;
    public LayerMask pusherLayer;
    private int defaultLayer = 0;
    private int pushLayer = 8;

    //Charge
    public float chargeTrackingTimer;
    private bool ifFailedChargeTime;
    private bool ifSuccesChargeTime;
    public float speedReducerMultiplier = 0.75f;


    //SlowMotion
    //public SlowMotion slowMotion;

    // Freezer
    //public Freezer freeze;
    
    //Audiosystem
    [Header("Audio")]
    private AudioManager audioManager;
    public AudioSource audioSourceChargedUp;
    public AudioSource audioSourcePullSounds;
    public AudioSource audioSourceAirPullSounds;
    public AudioClip[] pullSounds;
    public AudioClip[] airPullSounds;
    //private AudioMixer audioMixer;
    //private float pitchValue;
    //private float timeBox;
    //private float audioCoolDown;


    //Flash
    //public GameObject pusher;
    //public float flashTime = 0.075f;


    private IEnumerator powerupParticle;

    [SerializeField] private GameObject hitCircle;
    [SerializeField] private GameObject chargeCircle;
    



    /*public void SetPitch()
    {
        audioMixer.SetFloat("ExposedPitch", pitchValue);
        Debug.Log("Pitch Value: " + pitchValue);
        timeBox = Time.time;
    }*/


    void Start()
    {
        thePusher = GameObject.FindWithTag("Pusher");
        rigidbodyPusher = thePusher.GetComponent<Rigidbody2D>();
        pullField = gameObject.GetComponentInChildren<FieldTrigger>();
        boxColliderPusher = thePusher.GetComponent<BoxCollider2D>();
        chargeTrackingTimer = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        movement = GetComponent<PlayerMovement>();
        PushScript = thePusher.GetComponent<AbilityPower>();
        stun = GetComponent<Stunner>();
    }



    private void Update()
    {

        if (Respawn.pullerIsDead)
        {
            StopCoroutine(powerupParticle);
        }


        if (upAbilityPress && !ifSuccesChargeTime && !pullField.inField)
        {
            //audioManager.PlaySFX(audioManager.airPull);
            AirPullSounds();
        }

        /*
        if (Input.GetKeyDown(pullOnPress))
        {
            hasPressedPull = true;
        }


        if (Input.GetKeyUp(pullOnPress) && pullField.inField)
        {
            //pusher.GetComponent<SpriteRenderer>().enabled = false;
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
        //pusher.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void FixedUpdate()
    {
        ChargePulling(1f, extraForce);
       /* if (chargeTrackingTimer > minChargingTime)
        {
            if (Input.GetKey(abilityPress))
            {
                if (!audioSourceChargedUp.isPlaying)
                {
                    ChargeUpSound();
                }
                chargedUpParticles.Play();
            }

            if (Input.GetKeyUp(abilityPress))
            {
                audioSourceChargedUp.Stop();
                chargeTrackingTimer = 0;
            }
        }*/
        
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
                chargeTrackingTimer = 0;
                StopCoroutine(ChargedUpParticles());
                var emission = chargedUpParticles.emission;
                emission.rateOverTime = 100f;
            }
        } else 
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

    IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(time);
        boxColliderPusher.gameObject.layer = defaultLayer;
    }

    private void ThePull(float extraForce)
    {
        rigidbodyPusher.AddForce(-VectorBetween(thePusher) * abilityPowerForce * extraForce, ForceMode2D.Impulse);
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
            StartCoroutine(PushScript.SetIsHit());
            //audioManager.PlaySFX(audioManager.pull);
            PullSounds();

            Instantiate(hitCircle, thePusher.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

            CameraShake.Instance.ShakeCamera(CameraShakeValues.normalAbilityIntensity, CameraShakeValues.normalAbilityDuration);
        }

        if (ifSuccesChargeTime && pullField.inField)
        {
            ThePull(chargedPull);
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
            StartCoroutine(PushScript.SetIsHit());
            audioManager.PlaySFX(audioManager.chargePull);

            Instantiate(chargeCircle, thePusher.transform.position, Quaternion.identity);

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
            
            /*if (chargeTrackingTimer > minChargingTime)
            {
                    
                chargedUpParticles.Play();
                

                if (!audioSourceChargedUp.isPlaying)
                {
                    ChargeUpSound();
                }
            }*/

            chargeTrackingTimer += Time.deltaTime;
        }
        else if (upAbilityPress && chargeTrackingTimer > minChargingTime && pullField.inField)
        {
            ifSuccesChargeTime = true;
            //slowMotion.DoSlowmotion();
            //freeze.Freeze();
            //SetPitch();
        }
        else if (upAbilityPress && pullField.inField)
        {
            ifFailedChargeTime = true;
        }

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
    
    void PullSounds()
    {
        AudioClip clip = pullSounds[UnityEngine.Random.Range(0, pullSounds.Length)];
        audioSourcePullSounds.PlayOneShot(clip);
    }

    void AirPullSounds()
    {
        AudioClip clip = airPullSounds[UnityEngine.Random.Range(0, airPullSounds.Length)];
        audioSourceAirPullSounds.pitch = Random.Range(0.8f, 0.9f);
        audioSourceAirPullSounds.volume = (2f);
        audioSourceAirPullSounds.PlayOneShot(clip);
    }
}
