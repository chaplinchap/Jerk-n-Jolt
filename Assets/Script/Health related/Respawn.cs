using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //Varibles declared
    [Header("Pusher Settings")]
    public GameObject Pusher;
    public GameObject PusherRespawnPoint;
    public Health Push_healthScript; //reference to Health script
    
    [Header("Puller Settings")]
    public GameObject Puller;
    public GameObject PullerRespawnPoint;
    public Health Pul_healthScript; //reference to Health script
    
    
    private float respawnDelay = 1f; //Decide when should respawn

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pusher"))
        {
            Pusher.gameObject.SetActive(false);
            StartCoroutine(RespawnPusher());   
            Debug.Log("Player has fallen out");
        }

        //Puller player repspawn
        if (collision.gameObject.CompareTag("Puller"))
        {
            Puller.gameObject.SetActive(false);
            StartCoroutine(RespawnPuller());   
            Debug.Log("Player has fallen out");
        }
    }


    // METHODS //

    private void Spawn(GameObject player, GameObject spawner)
    {
        //Change players current position to another objects position
        player.transform.position = spawner.transform.position;
    }

    private IEnumerator RespawnPusher()
    {
        yield return new WaitForSeconds(respawnDelay);
        Spawn(Pusher, PusherRespawnPoint);

        // If player has more health it will spawn
        if (Push_healthScript.currentHealth > 0) {
            Pusher.gameObject.SetActive(true);
            Debug.Log("Player Spawns");
        }
        
        // If player has no more health left it will not spawn
        else {
            Debug.Log("The player is dead and will not spawn");
        }
    }

    private IEnumerator RespawnPuller()
    {
        yield return new WaitForSeconds(respawnDelay);
        Spawn(Puller, PullerRespawnPoint);
        
        // If player has more health it will spawn
        if (Pul_healthScript.currentHealth > 0) {
            Puller.gameObject.SetActive(true);
            Debug.Log("Player Spawns");
        }

        // If player has no more health left it will not spawn
        else {
            Debug.Log("The player is dead and will not spawn");
        }  
    }
}
