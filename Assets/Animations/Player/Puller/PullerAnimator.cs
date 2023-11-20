using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullerAnimator : AnimationsParent
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    [SerializeField] protected GameObject lineRenderer;

    const string idle = "PullerIdle";
    const string running = "PullerRunning";
    const string jumping = "PullerJumping";
    const string charge = "PullerCharge";
    const string attack = "PullerAttack";
    const string runningCharge = "PullerRunningCharge";
    const string jumpingCharge = "PullerJumpCharge";
    const string jumpingAttack = "PullerJumpAttack";
    const string falling = "PullerFalling";
    const string stun = "PullerStun";
    const string runningChargePenalty = "PullerPenaltyCharge";
    const string dashing = "PullerDashing";



    protected override void OnEnable()
    {
        base.OnEnable();

    }

    private void Update()
    {

        if (stunScript.IsStunned())
        {
            Stunned();
        }

        if (dashScript.IsDashing()) { 
            ChangeAnimationState(dashing);
        
        }



        if (!abilityPowerScript.HasPressedAbility() && isAttacking)
        {
            lineRenderer.SetActive(false);
            isAttackFinished = false;



            Invoke("AttackComplete", 0.1f);
        }

        if (abilityPowerScript.HasPressedAbility())
        {

            //lineRenderer.SetActive(true);

            isAttacking = true;
        }


    }


    private void FixedUpdate()
    {

        if (stunScript.IsStunned() || dashScript.IsDashing()) { return; }

        if (isAttacking && isAttackFinished && movementScript.IsGrounded())
        {

            if (movementScript.GetMovementX() != 0)
            {
                lineRenderer.SetActive(true);

                if (stunScript.IsPenalty()) {
                    ChangeAnimationState(runningChargePenalty);
                }
                else
                    ChangeAnimationState(runningCharge);

            }
            else
            {
                lineRenderer.SetActive(true);

                ChangeAnimationState(charge);
            }


        }

        if (movementScript.IsGrounded() && !isAttacking)
        {

            if (movementScript.GetMovementX() != 0)
            {
                ChangeAnimationState(running);
            }

            else if (movementScript.GetMovementX() == 0 && !abilityPowerScript.HasPressedAbility())
            {
                ChangeAnimationState(idle);
            }
        }


        if (!movementScript.IsGrounded() && isAttackFinished)
        {

            if (isAttacking)
            {
                ChangeAnimationState(jumpingCharge);
                lineRenderer.SetActive(true);
            }
            else
            {
                ChangeAnimationState(jumping);

            }
        }

        /*
        if (movementScript.rb.velocity.y > 0.1f && isAttackFinished) {

            ChangeAnimationState(falling);
        
        }
         */

    }





    // METHODS \\


    protected override void GetScripts()
    {
        base.GetScripts();

        line.SetUpLine(points);
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetActive(false);

    }

    private void Stunned()
    {
        ChangeAnimationState(stun);
        AttackComplete();
        lineRenderer.SetActive(false);
        return;
    }

}
