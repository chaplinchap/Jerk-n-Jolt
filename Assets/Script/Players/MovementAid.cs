
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAid : MonoBehaviour
{

    private const float DOUBLE_TAP_TIME = .15f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float duration = 2;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    float timeSinceLastTapLeft;
    float timeSinceLastTapRight;
    bool isDashing = false; 

    protected void Dashing() 
    {
        if (Input.GetKeyDown(playerMovement.moveLeft))
        {

            float timeBetween = Time.time - timeSinceLastTapLeft;


            if (timeBetween <= DOUBLE_TAP_TIME && !isDashing)
            {
                isDashing = true;
                rb.AddForce(Vector2.left * dashingPower, ForceMode2D.Impulse);
                StartCoroutine(Cooldown(duration));

                CameraShake.Instance.ShakeCamera(CameraShakeValues.dashingIntensity, CameraShakeValues.dashingDuration);


            }
           
            timeSinceLastTapLeft = Time.time;
        }

        if (Input.GetKeyDown(playerMovement.moveRight))
        {

            float timeBetween = Time.time - timeSinceLastTapRight;


            if (timeBetween <= DOUBLE_TAP_TIME && !isDashing)
            {
                isDashing = true;
                rb.AddForce(Vector2.right * dashingPower, ForceMode2D.Impulse);
                StartCoroutine(Cooldown(duration));

                CameraShake.Instance.ShakeCamera(CameraShakeValues.dashingIntensity, CameraShakeValues.dashingDuration);


            }
           
            timeSinceLastTapRight = Time.time;
        }


    }

    float timeSinceLastTap;
    protected void JumpDashing() 
    {

        if (Input.GetKeyDown(playerMovement.jumpUp))
        {
            float timeBetween = Time.time - timeSinceLastTap;

            if (timeBetween <= DOUBLE_TAP_TIME && !isDashing)
            {
                isDashing = true;
                rb.AddForce(Vector2.up * dashingPower, ForceMode2D.Impulse);
                StartCoroutine(Cooldown(duration));

                Debug.Log("Work)");

                CameraShake.Instance.ShakeCamera(CameraShakeValues.dashingIntensity, CameraShakeValues.dashingDuration);


            }
            
             timeSinceLastTap = Time.time;
        }

    }




    private IEnumerator Cooldown(float duration) 
    {
        yield return new WaitForSeconds(duration);
        isDashing = false; 

    }
}
