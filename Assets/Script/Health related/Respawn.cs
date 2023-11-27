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
    private AnimationsParent[] animationsParents = new AnimationsParent[2];

    public static bool pusherIsDead = false;
    public static bool pullerIsDead = false;


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
        //Pusher respawn
        if (collision.gameObject.CompareTag("Pusher"))
        {
           
            pusherIsDead = true;
            pusher.gameObject.SetActive(false);       //Flyt den her ind under RespawnPusher
            //StartCoroutine(DeathAnimationPusher());
            StartCoroutine(RespawnPusher());   
            Debug.Log("Player has fallen out");
           
        }

        //Puller respawn
        if (collision.gameObject.CompareTag("Puller"))
        {
            pullerIsDead = true;
            puller.gameObject.SetActive(false);
            //StartCoroutine(DeathAnimationPuller());q
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
        pusherIsDead = false;

        // If player has more health it will spawn
        if (pushHealthScript.currentHealth > 0) {
            pusher.gameObject.SetActive(true);
            //TurnScripts(true, 0);
            yield return new WaitForSeconds(.1f) ;
            TurnScripts(false, 0);
            yield return null; 
            TurnScripts(true, 0);

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
        pullerIsDead = false;

        // If player has more health it will spawn
        if (pullHealthScript.currentHealth > 0) {
            puller.gameObject.SetActive(true);
           
            //TurnScripts(true, 1);
            yield return new WaitForSeconds(.1f) ;
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
        //animationsParents[i] = player.GetComponent<AnimationsParent>();

    }


    protected virtual void TurnScripts(bool turn, int i)
    {
        playerMovements[i].enabled = turn;
        movementAids[i].enabled = turn;
        stunbarScripts[i].enabled = turn;
        abilityPowers[i].enabled = turn;
        stunners[i].enabled = turn;
        //animationsParents[i].enabled = turn;

    }

    IEnumerator DeathAnimationPusher()
    {
        yield return new WaitForSeconds(2f);
        pusher.gameObject.SetActive(false);
    }
    
    IEnumerator DeathAnimationPuller()
    {
        yield return new WaitForSeconds(2f);
        
    }

}
