using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Last3PlatformsFadeOnSD : MonoBehaviour
{

    public PlatformScriptableObject platformScriptableObject;
    public float durationTime = 1.5f;
    public float cancelFadeTime = 1f;

    [SerializeField] private bool onPlatformFade = false;
    [SerializeField] private bool offPlatformFade = false;
    [SerializeField] private bool platformDespawned = false;

    // public GameObject suddenDeathManager;
    // private DeathGameChange suddenDeath;

    private bool suddenDeathColorChangeTriggered = false;

    private IEnumerator despawnCoroutine;


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Puller") && DeathGameChange.suddenDeathTriggered || collision.gameObject.CompareTag("Pusher") && DeathGameChange.suddenDeathTriggered)
        {
            Debug.Log("Enter Happens");
            onPlatformFade = true;
            offPlatformFade = false;


            if (onPlatformFade && !offPlatformFade && !platformDespawned)
            {
                Debug.Log("starts Despawn");
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
            Debug.Log("Exit Happens");
            offPlatformFade = true;
            onPlatformFade = false;


            if (offPlatformFade && !onPlatformFade && !platformDespawned)
            {
                Debug.Log("Stops Despawn routine");
                StartCoroutine(CancelDespawn(gameObject, cancelFadeTime));
                offPlatformFade = false;
                onPlatformFade = false;
            }

        }

    }



    private void Start()
    {
       // suddenDeath = suddenDeathManager.GetComponent<DeathGameChange>();
    }

    void Update()
    {

        if(DeathGameChange.suddenDeathTriggered && !suddenDeathColorChangeTriggered)
        {
            suddenDeathColorChangeTriggered = true;
            StartCoroutine(ChangeColor(gameObject));
        }


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
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(time);
            platformScriptableObject.Fade(target);
        }

        Debug.Log("Despawning Platform");
        yield return new WaitForSeconds(time);
        platformScriptableObject.Despawn(target);
        platformDespawned = true;


    }

    public IEnumerator CancelDespawn(GameObject target, float time)
    {
        StopCoroutine(despawnCoroutine);
        Debug.Log("CancelDespawn Starts");
        yield return new WaitForSeconds(time);
        platformScriptableObject.CancelDespawn(target);
    }

    public IEnumerator Respawn(GameObject target, float time)
    {
        Debug.Log("Respawns Platform");
        yield return new WaitForSeconds(time * 2);
        platformScriptableObject.Spawn(target);
    }

    public IEnumerator ChangeColor (GameObject target)
    {
        yield return new WaitForSeconds(0f);
        platformScriptableObject.ChangeColor(target);
    }
}
