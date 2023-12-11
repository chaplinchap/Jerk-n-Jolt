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
    //private readonly int attack = Animator.StringToHash("PullerAttack");
    private readonly int runningCharge = Animator.StringToHash("PullerRunningCharge");
    private readonly int jumpingCharge = Animator.StringToHash("PullerJumpCharge");
    //private readonly int jumpingAttack = Animator.StringToHash("PullerJumpAttack");
    private readonly int dashing = Animator.StringToHash("PullerDashing");
    private readonly int stun = Animator.StringToHash("PullerStun");
    private readonly int runningChargePenalty = Animator.StringToHash("PullerPenaltyCharge");
    private readonly int spawning = Animator.StringToHash("PullerSpawning");
    private readonly int hit = Animator.StringToHash("PullerFalling");
    private readonly int hit2 = Animator.StringToHash("PullerHit2");
    private readonly int win1 = Animator.StringToHash("PullerWin");
    private readonly int win2 = Animator.StringToHash("PullerWin2");


    private int state;
    private float lockStateTimer;


    protected override void OnEnable() 
    {
        base.OnEnable();
        lineRenderer.SetActive(false);
    
    }

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
        if (isAttacking) return movementScript.IsGrounded() ? LockState(Attack(), 0) : LockState(JumpingAttack(), 0);
        if (dashScript.IsDashing()) return dashing;

        if (!movementScript.IsGrounded()) return isCharging ? JumpingCharge() : jumping;

        if (isCharging) return movementScript.GetMovementX() == 0 ? Charge() : (stunScript.IsPenalty() == true ? LockState(RunningChargePenalty(), 0.1f) : LockState(RunningCharge(), 0.1f));

        return movementScript.GetMovementX() == 0 ? idle : LockState(running, 0.1f);


        int LockState(int state, float time)
        {
            lockStateTimer = Time.time + time;
            return state;
        }
    }


    private int HitAnim() {

        lineRenderer.SetActive(false);

        int random = Random.Range(0, 2);

        switch (random) {

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
        lineRenderer.SetActive(false);

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

    private int Stun()
    {
        AttackComplete();
        lineRenderer.SetActive(false);
        return stun;
    }

    private int Attack() 
    {
        lineRenderer.SetActive(false);
        return 0;
    
    }


    private int JumpingAttack() 
    {
        lineRenderer.SetActive(false);
        return 0;
    }

    private int JumpingCharge() 
    {
        lineRenderer.SetActive(true);
        return jumpingCharge;
    }

    private int Charge() 
    {
        lineRenderer.SetActive(true);
        return charge;
    
    }

    private int RunningCharge() {

        lineRenderer.SetActive(true);
        return runningCharge;

    }


    private int RunningChargePenalty() 
    {
        lineRenderer.SetActive(true);
        return runningChargePenalty;
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
        //AttackComplete();
        lineRenderer.SetActive(false);
        return;
    }

}
 
