using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationsParent : MonoBehaviour
{

    protected Animator animator;
    protected string currentState;
    protected PlayerMovement movementScript;
    protected AbilityPower abilityPowerScript;
    protected Stunner stunScript;
    protected MovementAid dashScript;

    protected bool isAttacking = false;
    protected bool isAttackFinished = true;
    protected bool isRespawing = false; 


    void Start()
    {
        GetScripts();
    }

    protected virtual void OnEnable()
    {
        AttackComplete();
        Debug.Log("Animation Enable");
    }


    // METHODS \\

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }


    protected virtual void GetScripts()
    {
        animator = GetComponentInChildren<Animator>();
        movementScript = GetComponent<PlayerMovement>();
        abilityPowerScript = GetComponent<AbilityPower>();
        stunScript = GetComponent<Stunner>();
        dashScript = GetComponent<MovementAid>();
    }


    protected void AttackComplete()
    {
        isAttacking = false;
        isAttackFinished = true;
    }

    protected IEnumerator Respawn(float duration)
    {
        isRespawing = true;
        yield return new WaitForSeconds(duration);
        isRespawing = false;
    }
}
