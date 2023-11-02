using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private string currentState;
    private PlayerMovement move;
    private Push push;


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


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        push = GetComponent<Push>();
    }

    private void Update()
    {

        if (Input.GetKeyUp(push.pushOnPress) && isAttacking)
        {
            isAttackFinished = false;

            
            if (!move.IsGrounded())
            {
                ChangeAnimationState(jumpingAttack);

            }
             else{
            
                ChangeAnimationState(attack);
            }

            //float delay = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke("AttackComplete", 0.2f);
        }

        if (Input.GetKey(push.pushOnPress))
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {

        if (isAttacking && isAttackFinished && move.IsGrounded()) {

            if (move.rb.velocity.x !=0)
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

            else if (move.movementX == 0 && !Input.GetKey(push.pushOnPress))
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
