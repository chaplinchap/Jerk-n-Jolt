using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwitchPush : MonoBehaviour
{
    private GameObject theSwitchPuller;
  

    private Rigidbody2D rigidbodySwitchPuller;
    private FieldTrigger pushField;
    public float pushForce = 100;
    public Vector3 position;
    public Vector3 normVectorBetween;
    public KeyCode pushOnPress;
    private bool hasPressedPush = false;


  


    void Start()
    {
        theSwitchPuller = GameObject.FindWithTag("Pusher");
        rigidbodySwitchPuller = theSwitchPuller.GetComponent<Rigidbody2D>();
        pushField = gameObject.GetComponentInChildren<FieldTrigger>();       

    }

    private void Update()
    {
        if (Input.GetKeyDown(pushOnPress))
        {
            hasPressedPush = true;
        }

        if (Input.GetKeyUp(pushOnPress)) 
        {
            hasPressedPush = false;       
        }
    }

    private void FixedUpdate()
    {
        position = gameObject.GetComponent<Transform>().position;
        normVectorBetween = (theSwitchPuller.transform.position - position).normalized;


        if (pushField.inField && hasPressedPush)
        {
            rigidbodySwitchPuller.AddForce(normVectorBetween * pushForce, ForceMode2D.Impulse);
            hasPressedPush = false;
        }
    }
}
