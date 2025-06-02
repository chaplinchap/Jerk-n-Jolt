using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PullerAnimator : AnimationsParent
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    [SerializeField] protected GameObject lineRenderer;


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
        if (isRespawing) return LockState(_animData.Spawning, GetRespawnDuration() + 0.1f);
        if (stunScript.IsStunned()) return Stun();
        if (abilityPowerScript.IsHit()) return LockState(HitAnim(), abilityPowerScript.GetHitDuration() + 0.1f);
        if (isAttacking) return movementScript.IsGrounded() ? LockState(Attack(), 0) : LockState(JumpingAttack(), 0);
        if (dashScript.IsDashing()) return _animData.Dashing;

        if (!movementScript.IsGrounded()) return isCharging ? JumpingCharge() : _animData.Jumping;

        if (isCharging) return movementScript.GetMovementX() == 0 ? Charge() : (stunScript.IsPenalty() == true ? LockState(RunningChargePenalty(), 0.1f) : LockState(RunningCharge(), 0.1f));

        return movementScript.GetMovementX() == 0 ? _animData.Idle : LockState(_animData.Running, 0.1f);


        int LockState(int state, float time)
        {
            lockStateTimer = Time.time + time;
            return state;
        }
    }


    private int HitAnim() {

        lineRenderer.SetActive(false);

        return _animData.BeenHit;
    }

    private int WinAnim()
    {
        lineRenderer.SetActive(false);

        return _animData.Winning;
    }

    private int Stun()
    {
        AttackComplete();
        lineRenderer.SetActive(false);
        return _animData.Stun;
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
        return _animData.ChargeJumping;
    }

    private int Charge() 
    {
        lineRenderer.SetActive(true);
        return _animData.Charge;
    
    }

    private int RunningCharge() {

        lineRenderer.SetActive(true);
        return _animData.ChargeRunning;

    }


    private int RunningChargePenalty() 
    {
        lineRenderer.SetActive(true);
        return _animData.ChargeRunningPenalty;
    }



    // METHODS \\


    protected override void GetScripts()
    {
        base.GetScripts();

        line.SetUpLine(points);
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetActive(false);

    }

}
 
