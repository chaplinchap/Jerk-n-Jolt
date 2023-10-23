using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector2(0,1) * speed * Time.deltaTime);  
    }
}
