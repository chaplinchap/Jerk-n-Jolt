using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerSystemColor : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject triggerSwitch;
    private float timeBox;
    public float coolDown;
    public Color platformColor;
    public Color turnedOffColor;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
        {
            //Debug.Log("Something disappeared...");

            Platforms(false, turnedOffColor);
            timeBox = Time.time;
        }
    }


    // Update is called once per frame
    private void Update()
    {
        if (Time.time - timeBox > coolDown)
        {
            Platforms(true, platformColor);
        }
    }


    // METHODS //

    /** Platforms takes all the platforms in the level that is assigned to it and changes there state
     *  It takes in a bool that if true will set the platforms colliders to on
     *  Aswell as a color which it should turn into
     */
    private void Platforms(bool turn, Color changeColor)
    {


        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = turn;
            platforms[i].GetComponent<SpriteRenderer>().color = changeColor;
        }

        triggerSwitch.GetComponent<CircleCollider2D>().enabled = turn;
        triggerSwitch.GetComponent<SpriteRenderer>().enabled = turn;

    }
}