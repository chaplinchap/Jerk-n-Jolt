using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stunner : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private MovementAid movementAid;
    protected AbilityPower abilityPower;
    protected StunbarScript stunbarScript;

    [SerializeField] protected float stunTime;
    [SerializeField] protected float timeToStun;

    private float startingSpeed;
    private float duration;

    private float stunTimer;

    private bool isStunned; 

    protected void Awake()
    {
        GetScripts();
        startingSpeed = playerMovement.speed;
    }

    protected void OnEnable()
    {
            GetScripts();
        isPenalty = false; 
    }

    protected void Update()
    {
        Stun(timeToStun, stunTime, abilityPower.HasPressedAbility());
        stunbarScript.UpdateStunBar(GetTime(), timeToStun);
    }


    float time = 0;
    public void Slower(float penalty, float duration)
    {

        time += Time.deltaTime;
        Debug.Log(time);

        if (time > duration)
        {
            isPenalty = false;
            //playerMovement.speed /= penalty;
        }
    }

    bool isPenalty = false;
    protected void Stun(float timeToStun, float duration, bool isKeyPressed)
    {

        if (isPenalty)
        {
            return;
        }

        else if (isKeyPressed)
        {
            time += Time.deltaTime;

            //Debug.Log(time);

            if (time > timeToStun)
            {
                isPenalty = true;
                StartCoroutine(Stun(duration));
                time = 0;
                return;
            }
        }
        else if (!isKeyPressed)
        {
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

    public bool IsStunned() { return isStunned; }

    protected float GetTime() { return time; }

}