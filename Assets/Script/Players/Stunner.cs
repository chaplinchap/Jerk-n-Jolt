using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{

    public static Stunner Instance;

    private PlayerMovement playerMovement;
    private Push pushScript;
    private Pull pullScript;


    private float startingSpeed;


    private float duration;


    private void Awake()
    {

        Instance = this;
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
    public void Stun(float timeToStun, float duration, KeyCode button)
    {

        if (isPenalty)
        {
            return;
        }

        else if (Input.GetKey(button))
        {
            time += Time.deltaTime;

            Debug.Log(time);

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

    public IEnumerator Stun(float duration)
    {

        //this.duration = duration;

        TurnScripts(false);
        CameraShake.Instance.ShakeCamera(5f, .5f);

        yield return new WaitForSeconds(duration);

        TurnScripts(true);
        isPenalty = false;
    }


    // Only for debugging
    void Update()
    {

        Stun(pushScript.maxChargeingTime, pushScript.stunTime, pushScript.pushOnPress); 


    }


    private void GetScripts()
    {

        playerMovement = GetComponent<PlayerMovement>();

        try
        {
            pullScript = GetComponent<Pull>();
        }
        catch { }

        try
        {
            pushScript = GetComponent<Push>();
        }
        catch { }
    }


    private void TurnScripts(bool turn)
    {
        playerMovement.enabled = turn;

        try
        {
            pullScript.enabled = turn;
        }
        catch { }

        try
        {
            pushScript.enabled = turn;
        }
        catch { }
    }

}