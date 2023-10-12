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
             //Change players current position to another objects position
            Pusher.transform.position = PusherRespawnPoint.transform.position;
        }

        //Puller player repspawn
       if (collision.gameObject.CompareTag("Puller"))
        {
            //Change players current position to another objects position
            Puller.transform.position = PullerRespawnPoint.transform.position;
        }
       
    }
}
