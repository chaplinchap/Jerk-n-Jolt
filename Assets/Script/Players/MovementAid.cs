
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAid : MonoBehaviour
{

    private const float DOUBLE_TAP_TIME = .2f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float duration = 2;
    [SerializeField] private float dashingBuffer = .3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnEnable() 
    {
        isDashing = false;
        canDash = true;
    }


    private float timeSinceLastTapLeft;
    private float timeSinceLastTapRight;
    public bool isDashing = false; 
    public bool canDash = true;

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


    public bool IsDashing() { return isDashing; }
}
