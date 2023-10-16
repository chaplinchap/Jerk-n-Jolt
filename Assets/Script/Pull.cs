using System.Collections;
using UnityEngine;


public class Pull : MonoBehaviour
{
    private GameObject thePusher;
    private Rigidbody2D rigidbodyPusher;
    private FieldTrigger pullField;
    private BoxCollider2D boxColliderPusher;
 
    private float time = 0.15f;
    public float pullForce = 10;
    public KeyCode pullOnPress;
    private bool hasPressedPull = false;


    public LayerMask pusherLayer;

    private int defaultLayer = 0;
    private int pushLayer = 8;


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

    }

    private void FixedUpdate()
    {
        
        if (pullField.inField && hasPressedPull == true)
        {
           
            rigidbodyPusher.AddForce(-VectorBetween() * pullForce, ForceMode2D.Impulse);
            hasPressedPull = false;
            boxColliderPusher.gameObject.layer = pushLayer;
            StartCoroutine(ChangeLayer());
        }
    }

    IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(time);
        boxColliderPusher.gameObject.layer = defaultLayer;
    }

    private Vector3 VectorBetween() 
    {
        Vector3 position;

        position = gameObject.GetComponent<Transform>().position;
        return (thePusher.transform.position - position);
    }


}
