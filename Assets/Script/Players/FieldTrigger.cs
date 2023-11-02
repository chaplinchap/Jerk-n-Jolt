using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTrigger : MonoBehaviour
{

    public bool inField;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Puller") || other.gameObject.CompareTag("Pusher"))
        {

                   inField = true;

        }

    }


    void OnTriggerExit2D(Collider2D other)
    {
        inField = false;

    }

}
