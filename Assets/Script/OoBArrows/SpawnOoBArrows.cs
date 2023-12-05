using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOoBArrows : MonoBehaviour
{
    [SerializeField] private Image arrowPuller;
    [SerializeField] private Image arrowPusher;

    [SerializeField] protected bool isOOBPullerX;
    [SerializeField] protected bool isOOBPusherX;
    [SerializeField] protected bool isOOBPullerY;
    [SerializeField] protected bool isOOBPusherY;


    //[SerializeField] private GameObject arrow;


    private void Start()
    {
        //arrows = GetComponent<OoBArrow>();
        isOOBPullerX = false;
        isOOBPusherX = false;

        isOOBPullerX = false;
        isOOBPusherX = false;

    }


    protected bool InCornerPusher() 
    {
        if (isOOBPusherX && isOOBPusherY) return true;
        else return false;

    }

    protected bool InCornerPuller() 
    {
        if (isOOBPullerX && isOOBPullerY) return true;
        else return false;
    
    }


    protected void SetArrowCorner(Collider2D other) 
    {
        if (InCornerPusher()) 
        {
            ResetArrowX(other);
            ResetArrowY(other);
        }
    
    }


    protected void SetArrowX(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Push Arrow Spawned");
            isOOBPusherX = true;
            arrowPusher.enabled = true;

        }

        /*
        else if(!other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Push Arrow Despawned");
            isOOBPusherX = false;
            arrowPusher.enabled = false;
        }
         */


        if (other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Pull Arrow Spawned");
            isOOBPullerX = true;
            arrowPuller.enabled = true;

        }
        /*
        else if (isOOBPullerX && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Pull Arrow Despawned");
            isOOBPullerX = false;
            arrowPuller.enabled = false;
        }
         */
    }


    protected void SetArrowY(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPusherY = true;
            arrowPusher.enabled = true;

        }

        /*
        else if (isOOBPusherY && other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPusherY = false;
            arrowPusher.enabled = false;
        }
         */


        if (other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Spawned");
            isOOBPullerY = true;
            arrowPuller.enabled = true;

        }

        /*
        else if (isOOBPullerY && other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPullerY = false;
            arrowPuller.enabled = false;
        }
         */

    }


    protected void ResetArrowX(Collider2D other)
    {
        if(other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Push Arrow Despawned");
            isOOBPusherX = false;
            arrowPusher.enabled = false;
        }


         if (other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Pull Arrow Despawned");
            isOOBPullerX = false;
            arrowPuller.enabled = false;
        }
       
    }


    protected void ResetArrowY(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Pusher"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPusherY = false;
            arrowPusher.enabled = false;
        }
        

        if (other.gameObject.CompareTag("Puller"))
        {
            Debug.Log("Arrow Despawned");
            isOOBPullerY = false;
            arrowPuller.enabled = false;
        }

    }

}
