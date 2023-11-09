/*using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public ConsumV4 spawnedItemPositions; // Reference to the ConsumV4 script
    public float collisionRadius = 1.0f; // Adjust this value to match your needs

    private void Start()
    {
        // Find the VectorList GameObject in the scene
        spawnedItemPositions = GameObject.FindObjectOfType<ConsumV4>();
        
        if (spawnedItemPositions == null)
        {
            Debug.LogError("VectorList not found in the scene.");
        }
    }
    private void Update()
    {
        // Check for collisions
        CheckCollisions();
    }

    void CheckCollisions()
    {
        List<Vector2>  spawnedItemPositions = new List<Vector2>(); 

        foreach (Vector2 vector in spawnedItemPositions.spawnedItemPosition)
        {
            // Check if something collides with the vector position
            if (Vector2.Distance(transform.position, vector) < collisionRadius)
            {
                // Add the vector to the removal list
                vectorsToRemove.Add(vector);
                Debug.Log("Vector added");
            }
        }

        // Remove the vectors from the list
        foreach (Vector2 vector in vectorsToRemove)
        {
            spawnedItemPositions.spawnedItemPositions.Remove(vector);
            Debug.Log("Vector removed");
        }
    }
}

*/



