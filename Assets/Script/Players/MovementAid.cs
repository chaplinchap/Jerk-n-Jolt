
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

    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float duration = 2;
    [SerializeField] private float dashingBuffer = .3f;
    private AudioManager audioManager;
    public AudioSource audioSourceDash;
    
    [SerializeField] private float dashTime = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        dashBarScript = GetComponent<DashBarScript>();
    }

    void OnEnable() 
    {
        isDashing = false;
        canDash = true;
    }


    private float timeSinceLastTapLeft;
    private float timeSinceLastTapRight;
    private bool isDashing = false; 
    private bool canDash = true;

    protected void Dashing() 
    {

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

}
