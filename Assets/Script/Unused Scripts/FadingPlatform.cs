using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    

    
    /*

    private float timeboxPusher;
    private float timeboxPuller;

    public float standOnTime;
    private float newTimeboxPusher;
    private float newTimeboxPuller;

    public BoxCollider2D pusherCollider;
    public BoxCollider2D pullerCollider;

    private bool pusherStandingOnFade = false;
    private bool pullerStandingOnFade = false;

    private bool pusherStandOffFade = false;
    private bool pullerStandOffFade = false;

    private LayerMask PusherIgnoreFade = 13;
    private LayerMask PullerIgnoreFade = 14;
    private LayerMask defaultLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - timeboxPusher > standOnTime && pusherStandingOnFade == true)
        {
            
            pusherCollider.gameObject.layer = PusherIgnoreFade;
            newTimeboxPusher = Time.time;
            pusherStandOffFade = true;
            pusherStandingOnFade = false;
        }

        if (Time.time - timeboxPuller > standOnTime && pullerStandingOnFade == true)
        {
            pullerCollider.gameObject.layer = PullerIgnoreFade;
            newTimeboxPuller = Time.time;
            pullerStandOffFade = true;
            pullerStandingOnFade = false;
        }

        if (Time.time - newTimeboxPusher > 0.5f && pusherStandOffFade)
        {
            print(newTimeboxPusher);
            pusherCollider.gameObject.layer = defaultLayer;
            pusherStandOffFade = false;
            

        }

        if (Time.time - newTimeboxPuller > 0.5f && pullerStandOffFade)
        {
            pullerCollider.gameObject.layer = defaultLayer;            
            pullerStandOffFade = false;

        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
           if(collision.CompareTag("Pusher"))
           {
                timeboxPusher = Time.time;
                pusherStandingOnFade = true;
           }

           if(collision.CompareTag("Puller"))
           {
                timeboxPuller = Time.time;
                pullerStandingOnFade = true;
                
           }

        }
    }
    */


}
