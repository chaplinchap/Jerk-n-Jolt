using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{

    public PlatformScriptableObject platformScriptableObject;

    private float time;
    public float durationTime = 3f;

    private bool platformFade = true;

    private GameObject platform;


    private void Start()
    {
        platform = GetComponentInParent<GameObject>();
    }

    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher")) {

            Debug.Log("Enter");

            //time = Time.time;

            // platformScriptableObject.Despawn(gameObject);

            if (platformFade)
            {
                platformFade = false;
                StartCoroutine(Duration(platform, durationTime));
            }
        }

    }

     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {

            Debug.Log("Enter");

            //time = Time.time;

            // platformScriptableObject.Despawn(gameObject);

            if (platformFade)
            {
                platformFade = false;
                StartCoroutine(Duration(gameObject, durationTime));
            }
        }

    }


        void Update()
    {
        /*
        if (Time.time - time > 5) {

            platformScriptableObject.Spawn(gameObject);
        }
        */
    }


    public IEnumerator Duration(GameObject target, float time) {


        
        yield return new WaitForSeconds(time);
        platformScriptableObject.Fade(target);
        yield return new WaitForSeconds(time);
        platformScriptableObject.Fade(target);
        yield return new WaitForSeconds(time);
        platformScriptableObject.Fade(target);
        yield return new WaitForSeconds(time);
        platformScriptableObject.Despawn(target);
        yield return new WaitForSeconds(2 * time);
        platformScriptableObject.Spawn(target);

        platformFade = true;
    
    }


}
