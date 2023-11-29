using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerFade : MonoBehaviour
{

    public PlatformScriptableObject platformScriptableObject;
    public float durationTime = 0.25f;
    public float cancelFadeTime = 1f;

    [SerializeField] private bool onPlatformFade = false;
    [SerializeField] private bool offPlatformFade = false;
    [SerializeField] private bool platformDespawned = false;

    private IEnumerator despawnCoroutine;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
           
            onPlatformFade = true;
            offPlatformFade = false;


            if (onPlatformFade && !offPlatformFade && !platformDespawned)
            {
                
                despawnCoroutine = StartDespawn(gameObject, durationTime);
                StartCoroutine(despawnCoroutine);
                onPlatformFade = false;
                offPlatformFade = false;

            }

        }

    }

   
    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
           
            offPlatformFade = true;
            onPlatformFade = false;


            if (offPlatformFade && !onPlatformFade && !platformDespawned)
            {
                           
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
            yield return new WaitForSeconds(time);
            platformScriptableObject.Blink(target);
            yield return new WaitForSeconds(time);


        }
        platformScriptableObject.ChangeAlpha(target);
        
        Debug.Log("Despawning Platform");
        yield return new WaitForSeconds(time);
        platformScriptableObject.Despawn(target);
        platformDespawned = true;
              

    }

    public IEnumerator CancelDespawn (GameObject target, float time)
    {
        StopCoroutine(despawnCoroutine);
 
        yield return new WaitForSeconds(time);
        platformScriptableObject.CancelDespawn(target);
        
       
    }

    public IEnumerator Respawn(GameObject target, float time)
    {
        Debug.Log("Respawns Platform");
        yield return new WaitForSeconds(time * 2);
        platformScriptableObject.Spawn(target);     

    }
}
