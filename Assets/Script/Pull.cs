using System.Collections;
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

        if (Time.time - timebox > 0.25)
        {
            //boxColliderPusher.gameObject.layer = defaultLayer;
        }

    }

    private void FixedUpdate()
    {
        position = gameObject.GetComponent<Transform>().position;
        normVectorBetween = (position - thePusher.transform.position);


        if (pullField.inField && hasPressedPull == true)
        {
           
            rigidbodyPusher.AddForce(normVectorBetween * pullForce, ForceMode2D.Impulse);
            hasPressedPull = false;
            boxColliderPusher.gameObject.layer = pushLayer;
            timebox = Time.time;
            StartCoroutine(ChangeLayer());
        }
    }

    IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(0.25f);
        boxColliderPusher.gameObject.layer = defaultLayer;
    }
}
