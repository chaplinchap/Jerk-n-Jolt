using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PusherAnimator : AnimationsParent
{

    private float lockStateTimer;


    private int state;

    protected override void Update()
    {

        base.Update();

        state = GetState();

        ChangeAnimationState(state);
    }


    private int GetState()
    {
        if (Time.time < lockStateTimer) return currentState;

        if (UIManager.staticGameOver) return LockState(_animData.Winning, 2f);

        if (isRespawing) return LockState(_animData.Spawning, GetRespawnDuration() + 0.1f);
        if (stunScript.IsStunned()) return Stun();
        if (abilityPowerScript.IsHit()) return LockState(_animData.BeenHit, abilityPowerScript.GetHitDuration() + 0.1f);
        if (isAttacking) return movementScript.IsGrounded() ? LockState(_animData.Attack, attackDuration + 0.1f) : LockState(_animData.AttackJumping, attackDuration + 0.1f);
        if (dashScript.IsDashing()) return _animData.Dashing;

        if (!movementScript.IsGrounded()) return isCharging ? _animData.ChargeJumping : _animData.Jumping;

        if (isCharging) return movementScript.GetMovementX() == 0 ? _animData.Charge : (stunScript.IsPenalty() ? LockState(_animData.ChargeRunningPenalty, 0.1f) : LockState(_animData.ChargeRunning, 0.1f));

        return movementScript.GetMovementX() == 0 ? _animData.Idle : LockState(_animData.Running,0.1f); 
            

        int LockState(int state, float time)
        {
            lockStateTimer = Time.time + time;
            return state;
        }
    }



    private int Stun(){
        AttackComplete();
        isAttacking = false;
        return _animData.Stun; 
    }


}
