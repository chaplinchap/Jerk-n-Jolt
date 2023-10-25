using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SwitchPull : MonoBehaviour
{
    private GameObject swtichPusher;

    private Rigidbody2D rigidbodySwitchPusher;
    private FieldTrigger pullField;
    private BoxCollider2D boxColliderSwitchPusher;
 
    private float timebox;
    public float pullForce = 10;
    public Vector3 position;
    public Vector3 normVectorBetween;
    public KeyCode pullOnPress;
    private bool hasPressedPull = false;


    public LayerMask pusherLayer;

    private int defaultLayer = 0;
    private int pushLayer = 8;


    void Start()
    {
        swtichPusher = GameObject.FindWithTag("Puller");
        rigidbodySwitchPusher = swtichPusher.GetComponent<Rigidbody2D>();
        pullField = gameObject.GetComponentInChildren<FieldTrigger>();
        boxColliderSwitchPusher = swtichPusher.GetComponent<BoxCollider2D>();

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

        if (Time.time - timebox > 0.25)
        {
            boxColliderSwitchPusher.gameObject.layer = defaultLayer;
           
        }

    }

    private void FixedUpdate()
    {
        position = gameObject.GetComponent<Transform>().position;
        normVectorBetween = (position - swtichPusher.transform.position);


        if (pullField.inField && hasPressedPull == true)
        {
           
            rigidbodySwitchPusher.AddForce(normVectorBetween * pullForce, ForceMode2D.Impulse);
            hasPressedPull = false;
            boxColliderSwitchPusher.gameObject.layer = pushLayer;
            timebox = Time.time;
        }
    }
}