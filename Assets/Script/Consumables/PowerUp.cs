using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject push;
    private GameObject pull;

    private Push pushScript;
    private Pull pullScript;

    private void Awake() 
    {
        //push = GameObject.FindWithTag("Pusher");
        pull = GameObject.FindWithTag("Puller");

        pushScript = push.GetComponent<Push>();
        pullScript = pull.GetComponent<Pull>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher"))
        {   
            pushScript.extraForce = 50; 
            TriggerGone(this.gameObject);
            
            //StartCoroutine(PushPowerUp(10f, 100f));
        }

        if (collision.CompareTag("Puller"))
        {
            TriggerGone(this.gameObject);
        }
    }



    // METHODS \\

    private void TriggerGone(GameObject collision)   
    {
        Destroy(collision); 
    }

    private void PushPower() { 
    
        
    
    }
    
    /**
    private IEnumerator PushPowerUp(float time, float power) 
    {


        push.extraForce = power;  
        yield return new WaitForSeconds(time);
        push.extraForce = 1;
    }

     */


}
