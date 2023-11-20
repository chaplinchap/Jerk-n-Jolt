using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PullerAnimator : AnimationsParent
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    [SerializeField] protected GameObject lineRenderer;

    private readonly int idle = Animator.StringToHash("PullerIdle");
    private readonly int running = Animator.StringToHash("PullerRunning");
    private readonly int jumping = Animator.StringToHash("PullerJumping");
    private readonly int charge = Animator.StringToHash("PullerCharge");
    private readonly int attack = Animator.StringToHash("PullerAttack");
    private readonly int runningCharge = Animator.StringToHash("PullerRunningCharge");
    private readonly int jumpingCharge = Animator.StringToHash("PullerJumpCharge");
    private readonly int jumpingAttack = Animator.StringToHash("PullerJumpAttack");
    private readonly int dashing = Animator.StringToHash("PullerDashing");
    private readonly int falling = Animator.StringToHash("PullerFalling");
    private readonly int stun = Animator.StringToHash("PullerStun");
    private readonly int runningChargePenalty = Animator.StringToHash("PullerPenaltyCharge");
    private readonly int spawning = Animator.StringToHash("PullerSpawning");

    

    private void Update()
    {

        if (isRespawing) 
        {
            ChangeAnimationState(spawning);
            return; 
        }

        if (stunScript.IsStunned())
        {
            Stunned();
        }


        if (dashScript.IsDashing()) { 
            ChangeAnimationState(dashing);
            lineRenderer.SetActive(false);
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
        if (isRespawing) { return; }

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
 
