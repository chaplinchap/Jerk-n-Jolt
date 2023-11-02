using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter1 : MonoBehaviour
{
    public GameObject pusher;
    public GameObject puller;


    private Transform pusherTeleport;
    private Transform pullerTeleport;

    // Start is called before the first frame update
    void Start()
    {
        pusherTeleport = pusher.GetComponent<Transform>();
        pullerTeleport = puller.GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
            Vector3 newPos = new Vector3(31, 12, 1);

            if (collision.CompareTag("Pusher"))
            {
                pusherTeleport.transform.position = newPos;
            }

            if (collision.CompareTag("Puller"))
            {
                pullerTeleport.transform.position = newPos;
            }

        }
        
    }
}
