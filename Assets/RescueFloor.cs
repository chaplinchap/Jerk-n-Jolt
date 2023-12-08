using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueFloor : MonoBehaviour
{
    public GameObject rescueFloor;
    
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
