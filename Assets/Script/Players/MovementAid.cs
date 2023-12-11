
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAid : MonoBehaviour
{

    private const float DOUBLE_TAP_TIME = .2f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private DashBarScript dashBarScript;
    public ParticleSystem dashParticles;
    private Stunner stunScript;
    private AbilityPower abilityPowerScript;

    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float duration = 2;
    [SerializeField] private float dashingBuffer = .3f;

    [SerializeField] protected KeyCode dashButton; 

    private AudioManager audioManager;
    public AudioSource audioSourceDash;
    
    [SerializeField] private float dashTime = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        dashBarScript = GetComponentInChildren<DashBarScript>();
        stunScript = GetComponent<Stunner>();
        abilityPowerScript = GetComponent<AbilityPower>();
        dashBarScript.UpdateDashBar(1, 1);
    }

    void OnEnable() 
    {
        isDashing = false;
        canDash = true;
        //dashBarScript.UpdateDashBar(1, 1);
    }

    float time = 0;
    float t = 0;

    protected virtual void Update() 
    {

        //if (Respawn.pusherIsDead || Respawn.pullerIsDead) { dashBarScript.UpdateDashBar(0,1); return; }

        if (canDash) { return; }
        else if (isDashing) { time = 0; }
        else if (!canDash) { time += Time.deltaTime; }


        dashBarScript.UpdateDashBar(time, duration - dashingBuffer);

    }

    private float timeSinceLastTapLeft;
    private float timeSinceLastTapRight;
    private bool isDashing = false; 
    private bool canDash = true;


    protected void ToggleDash() 
    {
        
        if (stunScript.IsStunned() || abilityPowerScript.IsHit()) return; 

        if (Input.GetKeyDown(dashButton) && Input.GetKey(playerMovement.moveLeft) && canDash){

            DashDirection(Vector2.left);
        
        }

        if (Input.GetKeyDown(dashButton) && Input.GetKey(playerMovement.moveRight) && canDash) 
        {
            DashDirection(Vector2.right);
        }

    }


    protected void Dashing() 
    {

        if (stunScript.IsStunned() || abilityPowerScript.IsHit()) return; 


        if (Input.GetKeyDown(playerMovement.moveLeft))
        {

            float timeBetween = Time.time - timeSinceLastTapLeft;


            if (timeBetween <= DOUBLE_TAP_TIME && canDash)
            {
                DashDirection(Vector2.left);


            }

            timeSinceLastTapLeft = Time.time;
        }

        if (Input.GetKeyDown(playerMovement.moveRight))
        {

            float timeBetween = Time.time - timeSinceLastTapRight;


            if (timeBetween <= DOUBLE_TAP_TIME && canDash)
            {
                DashDirection(Vector2.right);


            }

            timeSinceLastTapRight = Time.time;
        }

    }

    float timeSinceLastTap;
    protected void JumpDashing() 
    {

        DashTimer(Vector2.up, playerMovement.jumpUp);

    }


    private void DashDirection(Vector2 direction) 
    {
        canDash = false;
        isDashing = true;
        rb.AddForce(direction * dashingPower, ForceMode2D.Impulse);
        StartCoroutine(Cooldown(duration));

        CameraShake.Instance.ShakeCamera(CameraShakeValues.dashingIntensity, CameraShakeValues.dashingDuration);

        //audioManager.PlaySFX(audioManager.dash);
        DashSound();
        dashParticles.Play();
    }


    private void DashTimer(Vector2 direction, KeyCode inputKey) 
    {
        if (Input.GetKeyDown(inputKey))
        {

            float timeBetween = Time.time - timeSinceLastTapLeft;


            if (timeBetween <= DOUBLE_TAP_TIME && canDash)
            {
                DashDirection(direction);


            }

            timeSinceLastTapLeft = Time.time;
        }

    }

    private IEnumerator Cooldown(float duration) 
    {
        yield return new WaitForSeconds(dashingBuffer);
        isDashing = false; 

        yield return new WaitForSeconds(duration - dashingBuffer);
        canDash = true;

    }


    public bool CanDash() { return canDash; }
    void DashSound()
    {
        audioSourceDash.pitch = UnityEngine.Random.Range(1f, 1.5f);
        audioSourceDash.volume = 0.40f;
        audioSourceDash.Play();
    }
    public bool IsDashing() { return isDashing; }

    public float TimeBetweenDash() { return duration - dashingBuffer; }

}
