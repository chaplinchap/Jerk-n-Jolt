
//using System;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathPlatformTriggerFade : MonoBehaviour
{

    public PlatformScriptableObject platformScriptableObject;
    private ParticleSystem particleSystem;
    private AudioManager audioManager;
    public float durationTime = 0.25f;
    public float respawnTime = 2f;
    public float cancelFadeTime = 1f;

    public bool onPlatformFade = false;
    public bool offPlatformFade = false;
    private bool platformDespawned = false;

    private IEnumerator despawnCoroutine;

    private bool suddenDeathColorChangeTriggered = false;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Puller") && DeathGameChange.suddenDeathTriggered || collision.gameObject.CompareTag("Pusher") && DeathGameChange.suddenDeathTriggered)
        {
            particleSystem.Play();
            audioManager.StartFloorShake();
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
        if (collision.gameObject.CompareTag("Puller") && DeathGameChange.suddenDeathTriggered || collision.gameObject.CompareTag("Pusher") && DeathGameChange.suddenDeathTriggered)
        {
            particleSystem.Stop();
            audioManager.StopFloorShake();
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

    void Update()
    {
        if (DeathGameChange.suddenDeathTriggered && !suddenDeathColorChangeTriggered)
        {
            suddenDeathColorChangeTriggered = true;
            StartCoroutine(ChangeColor(gameObject));
        }

        if (platformDespawned)
        {
            StartCoroutine(Respawn(gameObject, respawnTime));
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
        yield return new WaitForSeconds(time);
        platformScriptableObject.Spawn(target);     

    }

    public IEnumerator ChangeColor(GameObject target)
    {
        yield return new WaitForSeconds(0f);
        platformScriptableObject.ChangeColor(target);
    }
}
