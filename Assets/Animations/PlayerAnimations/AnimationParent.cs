using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class AnimationsParent : MonoBehaviour
{

    protected Animator animator;
    protected int currentState;
    protected PlayerMovement movementScript;
    protected AbilityPower abilityPowerScript;
    protected Stunner stunScript;
    protected MovementAid dashScript;
    //protected UIManager UImanager;

    protected bool isCharging = false;
    protected bool isAttacking = false;
    protected bool isRespawing = false;



    [SerializeField] private float respawnDuration = .3f;
    [SerializeField] protected float attackDuration = .3f;

    void Start()
    {
        GetScripts();
    }

    protected virtual void OnEnable()
    {
        currentState = 0;
        isCharging = false;
        isAttacking = false;
        //AttackComplete();
        StartCoroutine(Respawner(respawnDuration));
        //Debug.Log("Animation Enable");
    }

    protected virtual void Update()
    {


        if (abilityPowerScript.HasPressedAbility()) isCharging = IsCharging();

        else if (isCharging)
        {
           // Debug.Log("Starts coroutine");
            StartCoroutine(AttackComplete(attackDuration));
        }


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
        //UImanager = GetComponent<UIManager>();
    }

    protected void AttackComplete() 
    {
        isCharging = false;
        isAttacking = false;
    
    }


    protected IEnumerator AttackComplete(float duration)
    {
        isCharging = false; 
        isAttacking = true;
        yield return new WaitForSeconds(duration) ;
        isAttacking = false;
    }

    protected IEnumerator Respawner(float duration)
    {
        isRespawing = true;
        //Debug.Log("isRespawning: " + isRespawing);
        yield return new WaitForSeconds(duration);
        isRespawing = false;
        //Debug.Log("isRespawning: "+isRespawing);
    }

    protected float GetRespawnDuration() { return respawnDuration; }

    private bool IsCharging() 
    {
        if (stunScript.IsStunned()) return false;
        else return true;
    }


}
