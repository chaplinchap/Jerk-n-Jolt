using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private PlayerMovement move;
    private Pull pull;


    bool isAttacking = false;
    bool isAttackFinished = true;

    const string idle = "PullerIdle";
    const string running = "PullerRunning";
    const string jumping = "PullerJump";
    const string charge = "PullerCharging";
    const string attack = "PullerAttack";
    const string runningCharge = "PullerRunningCharge";
    const string jumpingCharge = "PullerJumpCharge";
    const string jumpingAttack = "PullerJumpAttack";
    const string falling = "PullerFalling";


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        pull = GetComponent<Pull>();
    }

    private void Update()
    {

        if (Input.GetKeyUp(pull.pullOnPress) && isAttacking)
        {
            isAttackFinished = false;


            if (!move.IsGrounded())
            {
                ChangeAnimationState(jumpingAttack);

            }
            else
            {

                ChangeAnimationState(attack);
            }

            //float delay = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke("AttackComplete", 0.2f);
        }

        if (Input.GetKey(pull.pullOnPress))
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {

        if (isAttacking && isAttackFinished && move.IsGrounded())
        {

            if (move.rb.velocity.x != 0)
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

            if (move.rb.velocity.x != 0)
            {
                ChangeAnimationState(running);
            }

            else if (move.movementX == 0 && !Input.GetKey(pull.pullOnPress))
            {
                ChangeAnimationState(idle);
            }
        }


        if (move.rb.velocity.y > 0.2f && isAttackFinished)
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


    void AttackComplete()
    {
        isAttacking = false;
        isAttackFinished = true;
    }
}
