using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Pull : MonoBehaviour
{
    private GameObject thePusher;
    private Rigidbody2D rigidbodyPusher;
    private FieldTrigger pullField;
    private BoxCollider2D boxColliderPusher;

    private float timebox;
    public float pullForce = 10;
    public Vector3 position;
    public Vector3 normVectorBetween;
    public KeyCode pullOnPress;
    private bool hasPressedPull = false;
    private bool hasPulled = false;



    void Start()
    {
        thePusher = GameObject.FindWithTag("Pusher");
        rigidbodyPusher = thePusher.GetComponent<Rigidbody2D>();
        pullField = gameObject.GetComponentInChildren<FieldTrigger>();
        boxColliderPusher = thePusher.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pullOnPress))
        {
         hasPressedPull = true;
        }

        if (Input.GetKeyUp(pullOnPress))
        {
            hasPressedPull = false;
        }

        if (Time.time - timebox > 1)
        {
            boxColliderPusher.enabled = true;
        }

    }

    private void FixedUpdate()
    {
        position = gameObject.GetComponent<Transform>().position;
        normVectorBetween = (position - thePusher.transform.position).normalized;


        if (pullField.inField && hasPressedPull == true)
        {
            rigidbodyPusher.AddForce(normVectorBetween * pullForce, ForceMode2D.Impulse);
            hasPressedPull = false;
            hasPulled = true;
            
        }
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pusher") && hasPulled)
        {
            boxColliderPusher.enabled = false;
            hasPulled = false;     
            timebox = Time.time;
           
        }


    }

  


}
