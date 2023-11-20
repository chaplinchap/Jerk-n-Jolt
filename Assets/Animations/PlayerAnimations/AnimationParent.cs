using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationsParent : MonoBehaviour
{

    protected Animator animator;
    protected int currentState;
    protected PlayerMovement movementScript;
    protected AbilityPower abilityPowerScript;
    protected Stunner stunScript;
    protected MovementAid dashScript;

    protected bool isAttacking = false;
    protected bool isAttackFinished = true;
    protected bool isRespawing = false;



    [SerializeField] private float respawnDuration = .3f;

    void Start()
    {
        GetScripts();
    }

    protected virtual void OnEnable()
    {
        currentState = 0;
        AttackComplete();
        StartCoroutine(Respawn(respawnDuration));
        //Debug.Log("Animation Enable");
    }


    // METHODS \\

    public void ChangeAnimationState(int newState)
    {
        if (currentState == newState) return;
        animator.CrossFade(newState, 0.05f, 0);
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
        //Debug.Log("isRespawning: " + isRespawing);
        yield return new WaitForSeconds(duration);
        isRespawing = false;
        //Debug.Log("isRespawning: "+isRespawing);
    }

    


}
