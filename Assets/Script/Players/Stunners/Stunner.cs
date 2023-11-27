using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stunner : MonoBehaviour
{
    AudioManager audioManager;
    private PlayerMovement playerMovement;
    private MovementAid movementAid;
    protected AbilityPower abilityPower;
    protected StunbarScript stunbarScript;

    [SerializeField] private float slowerTimeStunDivider = 2f;
    [SerializeField] private float penaltyMultiplier;
    [SerializeField] protected float stunTime;
    //[SerializeField] protected float timeToStun;

    private float startingSpeed;
    private float penaltySpeed;
    private float duration;

    private float time = 0;

    private float stunTimer;

    private bool isStunned;
    private bool isPenalty = false;
    private bool chargeReady;

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected void Awake()
    {
        GetScripts();
        startingSpeed = playerMovement.speed;
        penaltySpeed = playerMovement.speed / penaltyMultiplier;

    }

    protected void OnEnable()
    {
        Stun(.1f, 0, true);
        isStunned = false;
        isPenalty = false;
        playerMovement.speed = startingSpeed;
    }

    protected void Update()
    {
        Stun(abilityPower.maxChargeingTime, stunTime, abilityPower.HasPressedAbility());
        stunbarScript.UpdateStunBar(GetTime(), abilityPower.maxChargeingTime);

        if (abilityPower.IsHit() && !isStunned) {
            StartCoroutine(HitStun(abilityPower.GetHitDuration()));
        }

    }


    protected void Stun(float timeToStun, float duration, bool isKeyPressed)
    {

        if (isStunned || abilityPower.IsHit())
        {
            return;
        }



        else if (isKeyPressed)
        {
            time += Time.deltaTime;

            //Debug.Log(time);
        }
        else if (!isKeyPressed)
        {
            time = 0;
        }

        if (time > timeToStun / slowerTimeStunDivider)
        {
            isPenalty = true;
        }

        if (time < timeToStun / slowerTimeStunDivider)
        {
            isPenalty = false;
        }

        Slow();

        if (time > timeToStun)
        {
            //isPenalty = true;
            StartCoroutine(Stun(duration));
            time = 0;
        }

    }

    private IEnumerator Stun(float duration)
    {
        isStunned = true;
        TurnScripts(false);
        CameraShake.Instance.ShakeCamera(5f, .5f);

        yield return new WaitForSeconds(duration);

        isStunned = false;
        TurnScripts(true);
        isPenalty = false;
    }

    protected virtual void GetScripts()
    {

        playerMovement = GetComponent<PlayerMovement>();
        movementAid = GetComponent<MovementAid>();
        stunbarScript = GetComponentInChildren<StunbarScript>();
        abilityPower = GetComponent<AbilityPower>();
    }

    protected virtual void TurnScripts(bool turn)
    {
        playerMovement.enabled = turn;
        movementAid.enabled = turn;
        stunbarScript.enabled = turn;
        abilityPower.enabled = turn;

    }
    
    
    private void Slow() 
    {
        if (isPenalty)
        {
            playerMovement.speed = penaltySpeed;
            chargeReady = true;
            //PlayChargeAudio();
        }

        if (!isPenalty)
        {
            playerMovement.speed = startingSpeed;
            chargeReady = false;
        }

    }

    public void PlayChargeAudio()
    {
        if (chargeReady)
        {
            audioManager.PlaySFX(audioManager.charge);
            chargeReady = false;
        }
    }

    public IEnumerator HitStun(float duration) 
    {
        TurnScripts(false);
        yield return new WaitForSeconds(duration);
        TurnScripts(true);
    }

    public bool IsStunned() { return isStunned; }

    public bool IsPenalty() { return isPenalty; }

    protected float GetTime() { return time; }

}