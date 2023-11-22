using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PusherAnimator : AnimationsParent
{
    private readonly int idle = Animator.StringToHash("PusherIdle");
    private readonly int jumping = Animator.StringToHash("PusherJump");
    private readonly int running = Animator.StringToHash("PusherRunning");
    private readonly int charge = Animator.StringToHash("PusherCharging");
    private readonly int attack = Animator.StringToHash("PusherAttack");
    private readonly int runningCharge = Animator.StringToHash("PusherRunningCharge");
    private readonly int jumpingCharge = Animator.StringToHash("PusherJumpCharge");
    private readonly int jumpingAttack = Animator.StringToHash("PusherJumpAttack");
    private readonly int falling = Animator.StringToHash("PusherFalling");
    private readonly int stun = Animator.StringToHash("PusherStun");
    private readonly int dashing = Animator.StringToHash("PusherDashing");
    private readonly int runningChargePenalty = Animator.StringToHash("PusherPenaltyCharge");

    private readonly int spawning = Animator.StringToHash("PusherSpawning");

    private float lockStateTimer;
    



    protected override void Update() 
    {
        base.Update();

        int state = GetState();

        ChangeAnimationState(state);
    
    }


    private int GetState()
    {
        if (Time.time < lockStateTimer) return currentState;

        
        if (isRespawing) return LockState(spawning, GetRespawnDuration()+0.1f);
        if (stunScript.IsStunned()) return Stun(); 
        if (abilityPowerScript.IsHit()) return LockState(falling, abilityPowerScript.GetHitDuration());
        if (dashScript.IsDashing()) return dashing;

        if (isAttacking) return movementScript.IsGrounded() ? LockState(attack, attackDuration) : LockState(jumpingAttack, attackDuration) ;
        if (!movementScript.IsGrounded()) return isCharging ? jumpingCharge : jumping;

        if (isCharging) return movementScript.GetMovementX() == 0 ? charge : (stunScript.IsPenalty() == true ? runningChargePenalty : runningCharge);

        if (movementScript.GetMovementX() != 0) return running;

        return idle ;

        int LockState(int state, float time)
        {
            lockStateTimer = Time.time + time;
            return state;
        }
    }

    private int Stun(){
        AttackComplete();
        return stun; }

    /*
     
    private void Update()
    {

        // The Respawing Animation
        // This takes priority over other animations
        if (isRespawing) {
            //Debug.Log("Spawining Animation");
            ChangeAnimationState(spawning);
        }

        if (abilityPowerScript.IsHit()) 
        {
            ChangeAnimationState(falling);
        }

        // Stun Animation 
        if (stunScript.IsStunned())
        {
            ChangeAnimationState(stun);
            AttackComplete();
            return;
        }

        // Dash Animation
        if (dashScript.IsDashing()) { 
            ChangeAnimationState(dashing);
       
        }

        // Charging Animation
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

        // Setting the boolean of isAttacking when the attack button is pressed
        if (abilityPowerScript.HasPressedAbility())
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {

        // Making sure that no other animation plays when respawing
        if (isRespawing) {
            //Debug.Log("Animation FixedUpdate");
            return; }

        // Making surue that no other animiation plays upon stunning or dashing
        if (stunScript.IsStunned() || dashScript.IsDashing() || abilityPowerScript.IsHit()) { return; }


        // Sets the Animation for what charging animation should be played.
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

        // Grounded Animation
        if (movementScript.IsGrounded() && !isAttacking)
        {

            
            if (movementScript.GetMovementX() != 0) // if player is moving
            {
                ChangeAnimationState(running);
            }

            else if (movementScript.GetMovementX() == 0 && !abilityPowerScript.HasPressedAbility()) // if player is not moving and not charging
            {
                ChangeAnimationState(idle);
            }
        }

        // Player jump
        if (!movementScript.IsGrounded() && isAttackFinished)
        {

            if (isAttacking) // if player is charging while jumping
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
       
         

    }
    */

}
