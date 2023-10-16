using UnityEngine;

public class Respawn : MonoBehaviour
{
    //Varibles declared
    public GameObject PusherRespawnPoint;
    public GameObject PullerRespawnPoint;
    public GameObject Pusher;
    public GameObject Puller;

    //On collsion trigger respawn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Pusher player respawn
        if (collision.gameObject.CompareTag("Pusher"))
        {
            Spawn(Pusher, PusherRespawnPoint);
        }

        //Puller player repspawn
        if (collision.gameObject.CompareTag("Puller"))
        {
            Spawn(Puller, PullerRespawnPoint);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Pusher player respawn
        if (collision.gameObject.CompareTag("Pusher"))
        {
            Spawn(Pusher, PusherRespawnPoint);
        }

        //Puller player repspawn
        if (collision.gameObject.CompareTag("Puller"))
        {
            Spawn(Puller, PullerRespawnPoint);
        }

    }


    // METHODS //

    private void Spawn(GameObject player, GameObject spawner)
    {
        //Change players current position to another objects position
        player.transform.position = spawner.transform.position;
    }
}
