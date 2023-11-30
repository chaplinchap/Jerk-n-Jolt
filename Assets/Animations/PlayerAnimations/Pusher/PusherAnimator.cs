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
    private readonly int stun = Animator.StringToHash("PusherStun");
    private readonly int dashing = Animator.StringToHash("PusherDashing");
    private readonly int runningChargePenalty = Animator.StringToHash("PusherPenaltyCharge");
    private readonly int death = Animator.StringToHash("PusherDeath");
    private readonly int spawning = Animator.StringToHash("PusherSpawning");
    private readonly int hit = Animator.StringToHash("PusherFalling");
    private readonly int hit2 = Animator.StringToHash("PusherHit");
    private readonly int win1 = Animator.StringToHash("PusherWin");
    private readonly int win2 = Animator.StringToHash("PusherWin2");



    private float lockStateTimer;


    private int state;

    protected override void Update()
    {

        base.Update();

        state = GetState();

        ChangeAnimationState(state);
    }


    private void FixedUpdate()
    {
        
    }

    private int GetState()
    {
        if (Time.time < lockStateTimer) return currentState;

        if (UIManager.staticGameOver) return LockState(WinAnim(), 2f);

        if (isRespawing) return LockState(spawning, GetRespawnDuration() + 0.1f);
        if (stunScript.IsStunned()) return Stun();
        if (abilityPowerScript.IsHit()) return LockState(HitAnim(), abilityPowerScript.GetHitDuration() + 0.1f);
        if (isAttacking) return movementScript.IsGrounded() ? LockState(attack, attackDuration + 0.1f) : LockState(jumpingAttack, attackDuration + 0.1f);
        if (dashScript.IsDashing()) return dashing;

        if (!movementScript.IsGrounded()) return isCharging ? jumpingCharge : jumping;

        if (isCharging) return movementScript.GetMovementX() == 0 ? charge : (stunScript.IsPenalty() == true ? LockState(runningChargePenalty, 0.1f) : LockState(runningCharge, 0.1f));

        return movementScript.GetMovementX() == 0 ? idle : LockState(running,0.1f); 
            

        int LockState(int state, float time)
        {
            lockStateTimer = Time.time + time;
            return state;
        }
    }


    private int HitAnim()
    {

        int random = Random.Range(0, 2);

        switch (random)
        {

            case 0:
                return hit;

            case 1:
                return hit2;

            default:
                return 0;
        }
    }

    private int WinAnim()
    {

        int random = Random.Range(0, 2);

        switch (random)
        {

            case 0:
                return win1;

            case 1:
                return win2;

            default:
                return 0;
        }
    }

    private int Stun(){
        AttackComplete();
        return stun; 
    }

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
