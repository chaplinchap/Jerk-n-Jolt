using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject PusherRespawnPoint;
    public GameObject PullerRespawnPoint;
    public GameObject Pusher;
    public GameObject Puller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pusher")){
            Pusher.transform.position = PusherRespawnPoint.transform.position;
        }

       if (collision.gameObject.CompareTag("Puller"))
        {
            Puller.transform.position = PullerRespawnPoint.transform.position;
        }
       
    }
}
