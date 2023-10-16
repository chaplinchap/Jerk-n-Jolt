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
    }

    private void FixedUpdate()
    {
        if (pushField.inField && hasPressedPush)
        {
            rigidbodyPuller.AddForce(VectorBetween().normalized * pushForce, ForceMode2D.Impulse);
            hasPressedPush = false;
        }
    }

    private Vector3 VectorBetween()
    {
        Vector3 position;

        position = gameObject.GetComponent<Transform>().position;
        return (thePuller.transform.position - position);
    }
}
