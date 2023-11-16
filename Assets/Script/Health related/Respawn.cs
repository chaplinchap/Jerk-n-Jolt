using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    private PlayerMovement[] playerMovements = new PlayerMovement[2];
    private MovementAid[] movementAids = new MovementAid[2];
    private StunbarScript[] stunbarScripts = new StunbarScript[2];
    private AbilityPower[] abilityPowers = new AbilityPower[2];
    private Stunner[] stunners = new Stunner[2];

        


    public List<GameObject> randomSpawnPoints = new List<GameObject>();

    AudioManager audioManager;

    //Varibles declared
    [Header("Pusher Settings")]
    public GameObject pusher;
    // public GameObject PusherRespawnPoint;
    public HealthV2 pushHealthScript; //reference to Health script
    public ParticleSystem pusherRespawnParticles;
    
    [Header("Puller Settings")]
    public GameObject puller;
    // public GameObject PullerRespawnPoint;
    public HealthV2 pullHealthScript; //reference to Health script
    public ParticleSystem pullerRespawnParticles;
    private float respawnDelay = 1f; //Decide when should respawn


    // METHODS //


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        GetScripts(pusher, 0);
        GetScripts(puller, 1);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pusher"))
        {
           
            pusher.gameObject.SetActive(false);
            StartCoroutine(RespawnPusher());   
            Debug.Log("Player has fallen out");
        }

        //Puller player repspawn
        if (collision.gameObject.CompareTag("Puller"))
        {
            puller.gameObject.SetActive(false);
            StartCoroutine(RespawnPuller());   
            Debug.Log("Player has fallen out");
        }

    }


    private void Spawn(GameObject player, GameObject spawner)
    {
        //Change players current position to another objects position
        player.transform.position = spawner.transform.position;
    }

    private IEnumerator RespawnPusher()
    {
        int randomSpawnPoint = Random.Range(0, randomSpawnPoints.Count);
        GameObject selectedSpawnPoint = randomSpawnPoints[randomSpawnPoint];

        
        yield return new WaitForSeconds(respawnDelay);
        Spawn(pusher, selectedSpawnPoint);

        // If player has more health it will spawn
        if (pushHealthScript.currentHealth > 0) {
            pusher.gameObject.SetActive(true);
            //TurnScripts(true, 0);
            yield return null;
            TurnScripts(false, 0);
            yield return null; 
            TurnScripts(true, 0);
            Debug.Log("Player Spawns");
            audioManager.PlaySFX(audioManager.respawn);
            PusherRespawnParticles();
        }
        
        // If player has no more health left it will not spawn
        else {
            Debug.Log("The player is dead and will not spawn");
        }
    }

    private IEnumerator RespawnPuller()
    {
        int randomSpawnPoint = Random.Range(0, randomSpawnPoints.Count);
        GameObject selectedSpawnPoint = randomSpawnPoints[randomSpawnPoint];

        yield return new WaitForSeconds(respawnDelay);
        Spawn(puller, selectedSpawnPoint);
        
        // If player has more health it will spawn
        if (pullHealthScript.currentHealth > 0) {
            puller.gameObject.SetActive(true);
            //TurnScripts(true, 1);
            yield return null ;
            TurnScripts(false, 1);
            yield return null;
            TurnScripts(true, 1);
            Debug.Log("Player Spawns");
            audioManager.PlaySFX(audioManager.respawn);
            PullerRespawnParticles();
        }

        // If player has no more health left it will not spawn
        else {
            Debug.Log("The player is dead and will not spawn");
        }  
    }

    void PusherRespawnParticles()
    {
        pusherRespawnParticles.Play();
    }

    void PullerRespawnParticles()
    {
        pullerRespawnParticles.Play();
    }

    protected virtual void GetScripts(GameObject player, int i)
    {

        playerMovements[i] = player.GetComponent<PlayerMovement>();
        movementAids[i] = player.GetComponent<MovementAid>();
        stunbarScripts[i] = player.GetComponentInChildren<StunbarScript>();
        abilityPowers[i] = player.GetComponent<AbilityPower>();
        stunners[i] = player.GetComponent<Stunner>();
    }


    protected virtual void TurnScripts(bool turn, int i)
    {
        playerMovements[i].enabled = turn;
        movementAids[i].enabled = turn;
        stunbarScripts[i].enabled = turn;
        abilityPowers[i].enabled = turn;
        stunners[i].enabled = turn;

    }

}
