using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PusherAnimator : MonoBehaviour
{

    private Animator animator;
    private string currentState;
    private PlayerMovement move;
    private Push push;
    private Stunner stunScript;


    bool isAttacking = false;
    bool isAttackFinished = true;

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


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        push = GetComponent<Push>();
        stunScript = GetComponent<Stunner>();
    }

    private void Update()
    {

        if (stunScript.IsStunned())
        {
            ChangeAnimationState(stun);
            AttackComplete();
            return;
        }

        if (!push.HasPressedAbility() && isAttacking)
        {
            isAttackFinished = false;


            if (!move.IsGrounded())
            {
                ChangeAnimationState(jumpingAttack);

            }
            else {

                ChangeAnimationState(attack);
            }

            //float delay = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke("AttackComplete", 0.2f);
        }

        if (push.HasPressedAbility())
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {
        if (stunScript.IsStunned()) { return; }

        if (isAttacking && isAttackFinished && move.IsGrounded()) {

            if (move.GetMovementX() !=0)
            {

                ChangeAnimationState(runningCharge);

            }
            else
            {
                ChangeAnimationState(charge);
            }

        
        }

        if (move.IsGrounded() && !isAttacking)
        {

            if (move.GetMovementX() != 0)
            {
                ChangeAnimationState(running);
            }

            else if (move.GetMovementX() == 0 && !push.HasPressedAbility())
            {
                ChangeAnimationState(idle);
            }
        }


        if (!move.IsGrounded() && isAttackFinished)
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
        if (move.rb.velocity.y > 0.1f && isAttackFinished) {

            ChangeAnimationState(falling);
        
        }
         */

    }





    // METHODS \\

    public void ChangeAnimationState(string newState) 
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }


    void AttackComplete() { 
        isAttacking = false;
        isAttackFinished = true;
    }

}
