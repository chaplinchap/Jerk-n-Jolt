using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTheTextOnPlayer : MonoBehaviour
{

    [SerializeField]
    string textToSetPusher;

    [SerializeField] 
    string textToSetPuller;


    [SerializeField]
    TextInputScript scriptPusher;

    [SerializeField]
    TextInputScript scriptPuller;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Pusher"))
        {

            scriptPusher.SetText(textToSetPusher);

        }

        if (collision.CompareTag("Puller"))
        {
            if (textToSetPuller == "")
            {
                textToSetPuller = textToSetPusher;
            }

            scriptPuller.SetText(textToSetPuller);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Pusher"))
        {

            scriptPusher.SetText("");

        }

        if (collision.CompareTag("Puller"))
        {

            scriptPuller.SetText("");

        }


    }

}
