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


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pusher"))
        {

            collision.gameObject.GetComponentInChildren<TextInputScript>().SetText(textToSetPusher);

        }

        if (collision.CompareTag("Puller"))
        {

            collision.gameObject.GetComponentInChildren<TextInputScript>().SetText(textToSetPuller);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponentInChildren<TextInputScript>() != null)
        {

            collision.gameObject.GetComponentInChildren<TextInputScript>().SetText("");

        }


    }

}
