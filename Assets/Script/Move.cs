using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;           // platformens hastighed
    public int startingPoint;     // start position af platform
    public Transform[] points;    // et array af positioner some platformen skal bevæge sig på
    private int i;                // index af arrayet

    void Start()
    {
        transform.position = points[startingPoint].position;
    }
    
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
