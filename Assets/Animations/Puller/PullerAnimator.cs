using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private PlayerMovement move;
    private Pull pull;
    private FieldTrigger field;
    private Stunner stunScript;

    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    [SerializeField] private GameObject lineRenderer;




    public bool isAttacking = false;
    public bool isAttackFinished = true;

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


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMovement>();
        pull = GetComponent<Pull>();
        //field = GetComponentInChildren<FieldTrigger>();
        stunScript = GetComponent<Stunner>();

        line.SetUpLine(points);
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetActive(false);
    }

    private void Update()
    {

        if (stunScript.IsStunned())
        {
            ChangeAnimationState(stun);
            AttackComplete();
            lineRenderer.SetActive(false);
            return;
        }

        

        if (!pull.GetHasPressedPull() && isAttacking)
        {
            lineRenderer.SetActive(false);
            isAttackFinished = false;


            if (!move.IsGrounded())
            {
                //ChangeAnimationState(jumpingAttack);
                //lineRenderer.SetActive(true);  

            }
            else
            {

                //ChangeAnimationState(attack);
            }

            //float delay = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke("AttackComplete", 0.1f);
        }

        if (pull.GetHasPressedPull())
        {

            //lineRenderer.SetActive(true);
            
            isAttacking = true;
        }
         

    }


    private void FixedUpdate()
    {

        if (stunScript.IsStunned()) { return; }

        if (isAttacking && isAttackFinished && move.IsGrounded())
        {

            if (move.GetMovementX() != 0)
            {

                ChangeAnimationState(runningCharge);
                lineRenderer.SetActive(true);

            }
            else
            {
                ChangeAnimationState(charge);
                lineRenderer.SetActive(true);
            }


        }

        if (move.IsGrounded() && !isAttacking)
        {

            if (move.GetMovementX() != 0)
            {
                ChangeAnimationState(running);
            }

            else if (move.GetMovementX() == 0 && !pull.GetHasPressedPull())
            {
                ChangeAnimationState(idle);
            }
        }


        if (!move.IsGrounded() && isAttackFinished)
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
