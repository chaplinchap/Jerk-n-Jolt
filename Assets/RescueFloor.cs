using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueFloor : MonoBehaviour
{
    public GameObject rescueFloor;
    public static bool boostJumpActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        boostJumpActivated = false;
        rescueFloor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pusher") || other.CompareTag("Puller"))
        {
            rescueFloor.SetActive(true);
            boostJumpActivated = true;
        }
    }
}
