using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public GameObject pusher;
    public GameObject puller;
    public GameObject boost;
    public GameObject sprite;
    private float timeBox;
    public float originalSpeed;
    public float speedUp;
    public int coolDown;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher"))
        {
            Debug.Log("Speed Up boost activated");
            pusher.GetComponent<PlayerMovement>().speed *= speedUp;

            boost.GetComponent<CircleCollider2D>().enabled = false;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.02f);

            timeBox = Time.time;
        }

        if (collision.CompareTag("Puller"))
        {
            Debug.Log("Speed Up boost activated");
            puller.GetComponent<PlayerMovement>().speed *= speedUp;

            boost.GetComponent<CircleCollider2D>().enabled = false;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.02f);

            timeBox = Time.time;
        }
    }
    void Update()
    {
        if (Time.time - timeBox > coolDown)
        {
            puller.GetComponent<PlayerMovement>().speed = originalSpeed;
            puller.GetComponent<PlayerMovement>().speed = originalSpeed;

            boost.GetComponent<CircleCollider2D>().enabled = true;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1f);

        }
    }
}
