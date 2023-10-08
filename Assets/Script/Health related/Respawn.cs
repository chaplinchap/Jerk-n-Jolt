using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pusher")){
            Player.transform.position = respawnPoint.transform.position;
        }
    }
}
