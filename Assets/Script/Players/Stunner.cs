using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{


    private PlayerMovement playerMovement;


    private float startingSpeed;
    private float duration;

    private float stunTimer;

    private bool isStunned; 

    protected void Awake()
    {
        GetScripts();
        startingSpeed = playerMovement.speed;
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
    protected void Stun(float timeToStun, float duration, KeyCode button)
    {

        if (isPenalty)
        {
            return;
        }

        else if (Input.GetKey(button))
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
        else if (Input.GetKeyUp(button))
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

    }


    protected virtual void TurnScripts(bool turn)
    {
        playerMovement.enabled = turn;

    }

    public bool IsStunned() { return isStunned; }

}