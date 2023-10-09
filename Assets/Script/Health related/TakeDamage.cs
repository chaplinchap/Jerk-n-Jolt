using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pusher" || collision.tag == ("Puller"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
