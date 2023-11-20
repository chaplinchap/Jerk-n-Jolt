using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PusherAnimator : AnimationsParent
{
    const string idle = "PusherIdle";
    const string running = "PusherRunning";
    const string jumping = "PusherJump";
    const string charge = "PusherCharging";
    const string attack = "PusherAttack";
    const string runningCharge = "PusherRunningCharge";
    const string jumpingCharge = "PusherJumpCharge";
    const string jumpingAttack = "PusherJumpAttack";
    const string falling = "PusherFalling";
    const string stun = "PusherStun";
    const string runningChargePenalty = "PusherPenaltyCharge";
    const string dashing = "PusherDashing";

    const string spawning = "PusherJump";


    protected override void OnEnable()
    {
        StartCoroutine(Respawn(.4f)); 
        base.OnEnable();

    }

    private void Update()
    {
        if (isRespawing) {
            ChangeAnimationState(spawning);
            return; }

        if (stunScript.IsStunned())
        {
            ChangeAnimationState(stun);
            AttackComplete();
            return;
        }

        if (dashScript.IsDashing()) { 
            ChangeAnimationState(dashing);
       
        }

        if (!abilityPowerScript.HasPressedAbility() && isAttacking)
        {
            isAttackFinished = false;


            if (!movementScript.IsGrounded())
            {
                ChangeAnimationState(jumpingAttack);

            }
            else {

                ChangeAnimationState(attack);
            }

            //float delay = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke("AttackComplete", 0.2f);
        }

        if (abilityPowerScript.HasPressedAbility())
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {
        if (isRespawing) { return; }

        if (stunScript.IsStunned() || dashScript.IsDashing()) { return; }

        if (isAttacking && isAttackFinished && movementScript.IsGrounded()) {

            if (movementScript.GetMovementX() !=0)
            {

                if (stunScript.IsPenalty()) { 
                    ChangeAnimationState(runningChargePenalty);
                }
                else
                    ChangeAnimationState(runningCharge);

            }
            else
            {
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



}
