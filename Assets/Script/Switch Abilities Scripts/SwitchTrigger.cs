using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    public GameObject pusher; //for dragging the pusher GameObject into the inspector so we know what gameObject we want to "work" with
    public GameObject puller; //for dragging the puller GameObject into the inspector so we know what gameObject we want to "work" with
    public GameObject switchTrigger; //for dragging the switchtrigger gameObject into the inspector so we know what gameObject we want to "work" with
    public float respawnCD; //variable to tell when the switchTrigger should respawn
    public float timeStamp; //variable to "save" the time when we enter collision

    public Push pushObject; // makes a variable to save the pusher GameObject's Push script - the reason its public is so that we can use it in the other script
    public Pull pullObject; // makes a variable to save the puller GameObject's Pulle script - the reason its public is so that we can use it in the other script


    public GameObject pushFieldToDisable; //make variable to save the pushers pushField collider (ability range) - the reason its public is so that we can use it in the other script
    public GameObject pullFieldToEnable; //make variable to save the pullers pullField collider (ability range) - the reason its public is so that we can use it in the other script

    public GameObject pullFieldToDisable; //make variable to save the pushers "New" pullField collider (ability range) - the reason its public is so that we can use it in the other script
    public GameObject pushFieldToEnable; //make variable to save the pullers "New" pushField collider (ability range) - the reason its public is so that we can use it in the other script

    public bool hasSwitched = false; //condition for the first enterCollision trigger
    public bool isTriggeredOnce = false; //condition for the second enterCollision trigger (once its true)

    public Color pushColor; // color variable to save the pushers color
    public Color pullColor; // color variable to save the pullers color

    


    // Start is called before the first frame update
    void Start()
    {
        
        pushObject = pusher.GetComponent<Push>(); //saves the pusher GameObject into our variable
        pullObject = puller.GetComponent<Pull>(); //saves the puller GameObject into our variable

        pullFieldToDisable = GameObject.FindWithTag("PullField"); //finds and saves the pullers' pullField with tag into our variable.
        pushFieldToEnable = GameObject.FindWithTag("SwitchToPushField"); //finds and saves the pullers "new" pushField with tag into our variable.


        pushFieldToDisable = GameObject.FindWithTag("PushField"); // same as above but for the pusher
        pullFieldToEnable = GameObject.FindWithTag("SwitchToPullField");

        //we need to have these colliders enabled in unity - but when the game start we disable them (these are the new Range colliders, for when they switch abilities)
        pullFieldToEnable.SetActive(false); 
        pushFieldToEnable.SetActive(false);
        
        //same as the text above but for the scripts instead of the colliders
        pushObject.GetComponent<SwitchPull>().enabled = false;  //The script that gets enabled on the pusher so it now pulls
        pullObject.GetComponent<SwitchPush>().enabled = false; //The script that gets enabled on the puller so it now push



    }

    
    void Update()
    {
        // NUMBER 2:

        if (Time.time - timeStamp > respawnCD && hasSwitched == true && !isTriggeredOnce == true) //since we dont set hasSwitched to false in this body, we need !isTriggerOnce aswell otherwise the code in update, in the other script never happens
        {
            switchTrigger.GetComponent<CapsuleCollider2D>().enabled = true; //enables the collider again
            switchTrigger.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255, 1f); //change the color to teal (from red).
            isTriggeredOnce = true; //this is conditions for the "next" OnCollisionEnter in the other script

        }

    }


    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // NUMBER 1 - i made number 1, 2, 3, 4 so that number 1 happens first then 2 etc. : 

        if (hasSwitched == false)  //"default" conditions from beginning of game
        {
            pushColor = pushObject.GetComponent<SpriteRenderer>().color; // saves the pushers color into the variable
            pullColor = pullObject.GetComponent<SpriteRenderer>().color; // saves the pullers color into the variable

            if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
            {
                pushObject.GetComponent<SwitchPull>().enabled = true;   // Activate switchPuller script on the pusher
                pushObject.GetComponent<Push>().enabled = false;    // Deactivate push script
                                                                   

                pullObject.GetComponent<SwitchPush>().enabled = true;    // Activate switchPusher script on the puller
                pullObject.GetComponent<Pull>().enabled = false;    // Deactivate puller


                pullObject.GetComponent<SpriteRenderer>().color = pushColor; //sets the pullers color to the pushers to see that their ability has been changed.
                pushObject.GetComponent<SpriteRenderer>().color = pullColor; //sets the pushers color to the pullers to see that their ability has been changed.


                pullFieldToEnable.SetActive(true); //actives the switchPullField on the pusher - so it has pull range
                pushFieldToEnable.SetActive(true); // actives the switchPushField on the puller - so it has push range
                pushFieldToDisable.SetActive(false); //deactives the standard pushField on the pusher
                pullFieldToDisable.SetActive(false); // deactives the standard pullField on the puller

                switchTrigger.GetComponent<CapsuleCollider2D>().enabled = false; // disables the Collider for the trigger
                switchTrigger.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.02f); //make the trigger "fade"/transparent

                hasSwitched = true; //condition for the if() in void update(), above.
                
                timeStamp = Time.time; //timestamp to tell WHEN we entered the collider so we can use it to tell when to re-enable the trigger
               

            }
        }
    }
}
