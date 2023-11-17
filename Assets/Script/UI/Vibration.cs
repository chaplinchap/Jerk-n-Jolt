using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    public float wiggleAmount = 0.5f; // Adjust this value to control the intensity of the wiggle
    public float wiggleSpeed = 1f;   // Adjust this value to control the speed of the wiggle

    //Vector3 originalPosition;

    void Start()
    {
        //originalPosition = transform.position;
    }

    void Update()
    {
        // Generate random offsets for each axis
        float offsetX = Random.Range(-wiggleAmount, wiggleAmount);
        float offsetY = Random.Range(-wiggleAmount, wiggleAmount);
        float offsetZ = Random.Range(-wiggleAmount, wiggleAmount);

        // Calculate a sine wave to create a wiggling effect
        float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;

        // Apply the random offsets to the text's position
        //transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ) + new Vector3(wiggle, 0f, 0f);
        transform.position = gameObject.transform.position + new Vector3(wiggle, 0f, 0f);
    }
}