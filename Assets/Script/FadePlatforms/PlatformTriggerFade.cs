using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerFade : MonoBehaviour
{

    public PlatformScriptableObject platformScriptableObject;
    public float durationTime = 3f;
    public float cancelFadeTime = 1f;

    [SerializeField] private bool onPlatformFade = false;
    [SerializeField] private bool offPlatformFade = false;
    [SerializeField] private bool platformDespawned = false;

    private IEnumerator respawnCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Works");

        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Enter Happens");
            onPlatformFade = true;
            offPlatformFade = false;

            if (onPlatformFade && !offPlatformFade && !platformDespawned)
            {
                Debug.Log("starts Despawn");
                respawnCoroutine = StartDespawn(gameObject, durationTime);
                StartCoroutine(respawnCoroutine);
                onPlatformFade = false;
                offPlatformFade = false;

            }

        }

    }

   
    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Exit Happens");
            offPlatformFade = true;
            onPlatformFade = false;


            if (offPlatformFade && !onPlatformFade && !platformDespawned)
            {
                Debug.Log("Stops Despawn routine");
                //StopCoroutine(coroutine);
                //Invoke("platformScriptableObject.CancelDespawn(gameObject)", durationTime);
                StartCoroutine(CancelDespawn(gameObject, cancelFadeTime));
                offPlatformFade = false;
                onPlatformFade = false;
            }

        }

    }



    private void Start()
    {
       
    }

    void Update()
    {


        if (platformDespawned)
        {
            StartCoroutine(Respawn(gameObject, durationTime));
            platformDespawned = false;
            onPlatformFade = false;
            offPlatformFade = false;
        }



    }

    


    public IEnumerator StartDespawn(GameObject target, float time)
    {
        for(int i = 0; i <= 3; i++)
        {
            yield return new WaitForSeconds(time);
            platformScriptableObject.Fade(target);
        }
        
        Debug.Log("Despawning Platform");
        yield return new WaitForSeconds(time);
        platformScriptableObject.Despawn(target);
        platformDespawned = true;
              

    }

    public IEnumerator CancelDespawn (GameObject target, float time)
    {
        StopCoroutine(respawnCoroutine);
        Debug.Log("CancelDespawn Starts");
        yield return new WaitForSeconds(time);
        platformScriptableObject.CancelDespawn(target);
        
       
    }

    public IEnumerator Respawn(GameObject target, float time)
    {
        Debug.Log("Respawns Platform");
        yield return new WaitForSeconds(time);
        platformScriptableObject.Spawn(target);     

    }
}
