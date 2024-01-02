using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchTriggerBack : MonoBehaviour
{
    
    public GameObject switchTrigger;
   
    private SwitchTrigger switchNormal;
    
    public float timeStampBack;
    public bool isSwitchedBack = false;


  


    // Start is called before the first frame update
    void Start()
    {
        switchNormal = switchTrigger.GetComponent<SwitchTrigger>();   


    }

    // Update is called once per frame
    void Update()
    {
        //NUMBER 4

        if (Time.time - timeStampBack > switchNormal.respawnCD && isSwitchedBack == true)
        {
            
            switchTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
            switchTrigger.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1f);

            //resets all 3 bools back to "default" so we can start the whole process over once again.
            isSwitchedBack = false;          
            switchNormal.hasSwitched = false;
            switchNormal.isTriggeredOnce = false;

        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        //NUMBER 3

        if (switchNormal.isTriggeredOnce == true) //conditions for the onTriggerEnter, that gets set to true in void Update() in the other script
        {

            if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))

            {                                   
                switchNormal.pushObject.GetComponent<SwitchPull>().enabled = false;   // Disables switchPuller script on the pusher
                switchNormal.pushObject.GetComponent<Push>().enabled = true;    // Active push script on pusher


                switchNormal.pullObject.GetComponent<SwitchPush>().enabled = false;    // Disables switchPusher script on the puller
                switchNormal.pullObject.GetComponent<Pull>().enabled = true;    // Actives puller script on puller

                switchNormal.pullFieldToEnable.SetActive(false); //Disables the switchPullField on the pusher
                switchNormal.pushFieldToEnable.SetActive(false); //Disables the switchPushField on the puller
                switchNormal.pushFieldToDisable.SetActive(true); //Re-enable the normal pushField on the pusher
                switchNormal.pullFieldToDisable.SetActive(true); //Re-enable the normal pullField on the puller
        
                switchTrigger.GetComponent<CapsuleCollider2D>().enabled = false;
                switchTrigger.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255, 0.01f);

                switchNormal.pushObject.GetComponent<SpriteRenderer>().color = switchNormal.pushColor; //switches back to original color
                switchNormal.pullObject.GetComponent<SpriteRenderer>().color = switchNormal.pullColor; //switches back to original color


                timeStampBack = Time.time;
                isSwitchedBack = true; //condtion for the the trigger to "respawn", in void Update() in this script, above
                       
            }

        }
        
    }



}
