using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHp : MonoBehaviour
{
    [SerializeField] private float hp;

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        if (collision.CompareTag("Pusher") || collision.CompareTag("Puller")) //When one of the player is hit 
        {
            collision.GetComponent<Health>().GiveHP(hp); //The hit player gets health back
            this.gameObject.SetActive(false); //object will disapper
        }
    }
}
