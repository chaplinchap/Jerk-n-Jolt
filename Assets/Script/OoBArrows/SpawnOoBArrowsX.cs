using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOoBArrowsX : MonoBehaviour
{

    // Make Arrows appear when a player goes out of bounds.

    //private OoBArrow arrows;

    [SerializeField] private GameObject arrowPuller;
    [SerializeField] private GameObject arrowPusher; 

    private bool isOOBPullerX;
    private bool isOOBPusherX;


    //[SerializeField] private GameObject arrow;


    private void Start() 
    {
        //arrows = GetComponent<OoBArrow>();
        isOOBPullerX = false;
        isOOBPusherX = false; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!isOOBPusherX && other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPusherX = true;
            arrowPusher.SetActive(true);

        }
        else if (isOOBPusherX && other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPusherX = false; 
            arrowPusher.SetActive(false);
        }


        if (!isOOBPullerX && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPullerX = true;
            arrowPuller.SetActive(true);

        }
        else if (isOOBPullerX && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPullerX = false;
            arrowPuller.SetActive(false);
        }



    }

    /*

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (!isOOBPusher && other.gameObject.CompareTag("Pusher"))
        {

            Debug.Log("Arrow Spawned");
            isOOBPusher=true;
            rbPusher = other.GetComponent<Rigidbody2D>();


            Instantiate(arrow, rbPusher.transform.position, Quaternion.identity);

        }

        if (isOOBPusher && other.gameObject.CompareTag("Pusher")) 
        {
            arrows.DestroyThisArrow();
        }


    }


     */







}
