using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUP : MonoBehaviour
{
    public GameObject pusher;
    public GameObject puller;
    public GameObject boost;
    public GameObject sprite;
    private float timeBox;
    public float originalJump;
    public float jumpUp;
    public int coolDown;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher"))
        {
            Debug.Log("Jump Up boost activated");
            pusher.GetComponent<PlayerMovement>().jumpingPower *= jumpUp;

            boost.GetComponent<CircleCollider2D>().enabled = false;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.02f);

            timeBox = Time.time;

        }

        if (collision.CompareTag("Puller"))
        {
            Debug.Log("Jump Up boost activated");
            puller.GetComponent<PlayerMovement>().jumpingPower *= jumpUp;

            boost.GetComponent<CircleCollider2D>().enabled = false;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.02f);

            timeBox = Time.time;

        }

    }
    void Update()
    {
        if (Time.time - timeBox > coolDown)
        {
            pusher.GetComponent<PlayerMovement>().jumpingPower = originalJump;
            puller.GetComponent<PlayerMovement>().jumpingPower = originalJump;

            boost.GetComponent<CircleCollider2D>().enabled = true;
            sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
    }
}