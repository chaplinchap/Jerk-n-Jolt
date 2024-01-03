using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstatiateMissiles : MonoBehaviour
{

    public GameObject missileAgainstPuller;
    public GameObject missileAgainstPusher;
    private SpriteRenderer spriteRenderer;
    public List<GameObject> missileSpawnLocations = new List<GameObject>();
    public int missileSpawnCount = 6; // Amount of missiles to be spawned

    private bool startOnce; //Ensure no dublicate

    private void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Start Coroutine against the targetet player
        if (!startOnce)
        {   
            if (collision.CompareTag("Pusher"))
            {
                startOnce = true;
                StartCoroutine(SpawnMissilesAgainstPull());
            } 

            if (collision.gameObject.tag == "Puller")
            {
                startOnce= true;
                StartCoroutine(SpawnMissilesAgainstPush());
            }    
        }
    }

     IEnumerator SpawnMissilesAgainstPull()
     {
        spriteRenderer.color = new Color(1f,1f,1f,0f); // Consumable gets 0% opacity
        for (int i = 0; i < missileSpawnCount; i++)
        {
            // Choose a random position from the list
            int randomIndex = Random.Range(0, missileSpawnLocations.Count);
            GameObject spawnLocation = missileSpawnLocations[randomIndex];

            // Instantiate the missilePrefab at the chosen position
            Instantiate(missileAgainstPuller, spawnLocation.transform.position, Quaternion.identity);

            // Introduce a delay before the next missile spawns
            yield return new WaitForSeconds(1.0f);
        }
        startOnce = false; // enable coroutine targeting of player again
        spriteRenderer.color = new Color(1f,1f,1f,1f); // Consumable gets 100% opacity
    }

    IEnumerator SpawnMissilesAgainstPush()
     {
        spriteRenderer.color = new Color(1f,1f,1f,0f); // Consumable gets 0% opacity
        for (int i = 0; i < missileSpawnCount; i++)
        {
            // Choose a random position from the list
            int randomIndex = Random.Range(0, missileSpawnLocations.Count);
            GameObject spawnLocation = missileSpawnLocations[randomIndex];

            // Instantiate the missilePrefab at the chosen position
            Instantiate(missileAgainstPusher, spawnLocation.transform.position, Quaternion.identity);

            // Introduce a delay before the next missile spawns
            yield return new WaitForSeconds(1.0f);
        }
        startOnce = false; // enable coroutine targeting of player again
        spriteRenderer.color = new Color(1f,1f,1f,1f); // Consumable gets 100% opacity
    }

}
