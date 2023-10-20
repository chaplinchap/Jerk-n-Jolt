using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float wave = 0.25f; //How much it will float up and down
    public float speed = 1f; //The speed at which it will float
    Vector3 Pos;

    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position; //Finds the objects location
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Pos.x,Mathf.Sin(Time.time* speed) * wave + Pos.y, 0);
    }
}
