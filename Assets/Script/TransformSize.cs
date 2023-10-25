using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSize : MonoBehaviour
{
    public GameObject pusher;
    public GameObject puller;


    private Transform pusherSize;
    private Transform pullerSize;

    // Start is called before the first frame update
    void Start()
    {
        pusherSize = pusher.GetComponent<Transform>();
        pullerSize = puller.GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
            Vector3 newSize = new Vector3(3, 3, 1);
            pusherSize.transform.localScale = newSize;
            pullerSize.transform.localScale = newSize;

        }
        
    }
}
