using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private string currentState;
    private PlayerMovement move;
    private Push push;


    bool isAttacking = false;

    const string idle = "PusherIdle";
    const string running = "PusherRunning";
    const string jumping = "PusherJump";
    const string charge = "PusherCharging";
    const string attack = "PusherAttack";


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        push = GetComponent<Push>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(push.pushOnPress))
        {
            ChangeAnimationState(attack);
            
        }
    }


    private void FixedUpdate()
    {

        if (Input.GetKey(push.pushOnPress))
        {
            ChangeAnimationState(charge);

            isAttacking = true;


        }

        if (move.movementX == -1 && move.IsGrounded() || move.movementX == 1 && move.IsGrounded()) 
            ChangeAnimationState(running);
        

        if(move.movementX == 0 && move.IsGrounded() && !Input.GetKey(push.pushOnPress) && !isAttacking)
            ChangeAnimationState (idle);

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


}
