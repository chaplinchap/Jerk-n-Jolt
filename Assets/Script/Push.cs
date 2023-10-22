using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Push : MonoBehaviour
{
    private GameObject thePuller;
    private Rigidbody2D rigidbodyPuller;
    private FieldTrigger pushField;
    public float pushForce = 100;
    public KeyCode pushOnPress;
    private bool hasPressedPush = false;

    private float timer;
    public float chargingTime = 2f;

    private bool isCharging;
    private bool hasCharged; 

    void Start()
    {
        thePuller = GameObject.FindWithTag("Puller");
        rigidbodyPuller = thePuller.GetComponent<Rigidbody2D>();
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

        Timer(); 
    }

    private void FixedUpdate()
    {
        ChargePush();
    }


        // METHODS //   

    private Vector3 VectorBetween()
    {
        Vector3 position;

        position = gameObject.GetComponent<Transform>().position;
        return (thePuller.transform.position - position);
    }

    private void ThePush(float extraForce) 
    {
        rigidbodyPuller.AddForce(VectorBetween().normalized * pushForce * extraForce, ForceMode2D.Impulse);
        hasPressedPush = false;
    }

    private void Pushing()
    {
        if (pushField.inField && hasPressedPush)
        {
            ThePush(1);
        }
    }


    private void ChargePush() 
    {
        if (isCharging && pushField.inField)
        {
            ThePush(1);
            isCharging = false;
            hasCharged = false;
        }

        if (hasCharged && pushField.inField)
        {
            ThePush(10);
            isCharging = false;
            hasCharged = false;
        }
    }

    private void Timer()
    {

        if (Input.GetKeyDown(pushOnPress))
        {
            timer = 0;
            isCharging = false;
            hasCharged = false;
        }
        else if (Input.GetKeyUp(pushOnPress) && timer > chargingTime && pushField.inField)
        {
            hasCharged = true;
        }
        else if (Input.GetKeyUp(pushOnPress) && pushField.inField)
        {
            isCharging = true;
        }

        if (Input.GetKey(pushOnPress))
        {
            timer += Time.deltaTime;
        }

    }
}
