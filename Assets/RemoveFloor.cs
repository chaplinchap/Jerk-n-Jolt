using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (RescueFloor.boostJumpActivated)
        {
            Debug.Log("called");
            gameObject.SetActive(false);
        }
        
    }
}
