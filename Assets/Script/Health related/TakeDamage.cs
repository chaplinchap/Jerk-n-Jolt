using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float damage; //Input how much damage to take

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller")) //When one of the player is hit 
        {
            collision.GetComponent<Health>().TakeDamage(damage); //The hit player takes damage
        }
    }
}
