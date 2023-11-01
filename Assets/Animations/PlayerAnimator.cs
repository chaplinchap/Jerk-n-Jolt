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


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        push = GetComponent<Push>();
    }

    private void Update()
    {

        if (Input.GetKeyUp(push.pushOnPress) )
        {
            isAttackFinished = false;

            ChangeAnimationState(attack);

            Invoke("AttackComplete", 0.2f);
        }

        if (Input.GetKey(push.pushOnPress))
        {
            isAttacking = true;
        }

    }


    private void FixedUpdate()
    {


        if (move.movementX != 0 && move.IsGrounded() && !isAttacking)
        {
                ChangeAnimationState(running);
        }


        if (move.movementX == 0 && move.IsGrounded() && !isAttacking && !Input.GetKey(push.pushOnPress))
        {
                ChangeAnimationState(idle);
        }

        if (isAttacking && isAttackFinished) {

            if (move.IsGrounded() && move.movementX == 0)
            {

                ChangeAnimationState(charge);

            }
            else if (move.IsGrounded()) 
            {
                ChangeAnimationState(runningCharge);
            }

        
        }


        if (move.rb.velocity.y > 0.2f) {
            ChangeAnimationState(jumping);
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
