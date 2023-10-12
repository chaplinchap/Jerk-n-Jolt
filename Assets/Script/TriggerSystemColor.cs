using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    public GameObject[] platform;
    public GameObject trigger;
    private float timeBox;
    public float coolDown;
    public Color platformColor; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
            Debug.Log("Something disappeared...");

            for (int i = 0; i < platform.Length; i++) 
            {
                platform[i].GetComponent<BoxCollider2D>().enabled = false;
                platform[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.02f);
            }

            trigger.GetComponent<CircleCollider2D>().enabled = false;
            trigger.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.02f);
            timeBox = Time.time;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeBox > coolDown)
        {

            for (int i = 0; i < platform.Length; i++)
            {
                platform[i].GetComponent<BoxCollider2D>().enabled = true;
                platform[i].GetComponent<SpriteRenderer>().color = platformColor;
            }
            
            trigger.GetComponent<CircleCollider2D>().enabled = true;
            trigger.GetComponent<SpriteRenderer>().color = platformColor;
        }
    }
}