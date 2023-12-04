using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOoBArrowsY : MonoBehaviour
{
    // Make Arrows appear when a player goes out of bounds.

    //private OoBArrow arrows;

    [SerializeField] private GameObject arrowPuller;
    [SerializeField] private GameObject arrowPusher;

    private bool isOOBPullerY;
    private bool isOOBPusherY;


    //[SerializeField] private GameObject arrow;


    private void Start()
    {
        //arrows = GetComponent<OoBArrow>();
        isOOBPullerY = false;
        isOOBPusherY = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!isOOBPusherY && other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPusherY = true;
            arrowPusher.SetActive(true);

        }
        else if (isOOBPusherY && other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPusherY = false;
            arrowPusher.SetActive(false);
        }


        if (!isOOBPullerY && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPullerY = true;
            arrowPuller.SetActive(true);

        }
        else if (isOOBPullerY && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPullerY = false;
            arrowPuller.SetActive(false);
        }



    }
}
