using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    public GameObject platform;
    public GameObject trigger;
    private float timeBox;
    public float coolDown;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
            Debug.Log("Something disappeared...");
            platform.GetComponent<BoxCollider2D>().enabled = false;
            trigger.GetComponent<CircleCollider2D>().enabled = false;
            platform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.02f);
            trigger.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.02f);
            timeBox = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeBox > coolDown) 
        {
            platform.GetComponent<BoxCollider2D>().enabled = true;
            trigger.GetComponent<CircleCollider2D>().enabled = true;
            platform.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0, 1f);
            trigger.GetComponent<SpriteRenderer>().color = new Color(255f, 255, 0, 1f);
        }
    }
}
