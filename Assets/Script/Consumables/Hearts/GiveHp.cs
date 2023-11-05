using Unity.VisualScripting;
using UnityEngine;

public class GiveHp : MonoBehaviour
{
    [SerializeField] private float hp = 1;
    private float despawnTime = 20f;
    private float SpawnTime;

  

    private void OnTriggerEnter2D(Collider2D collision) //Trigger on collision
    {
        if(collision.CompareTag("Pusher") || collision.CompareTag("Puller")) //When one of the player is hit 
        {
            collision.GetComponent<Health>().GiveHP(hp); //The hit player gets health back
         
            Destroy(this.gameObject); // Object will be destroyed
           
        }
    }

    private void Awake()
    {
        
        SpawnTime = Time.time + despawnTime;
    }

    private void Update()
    {
         if(SpawnTime < Time.time){
            // consumbeScript.currentConsumables--;
            Destroy(this.gameObject);            
            Debug.Log ("Despawned Heart");
            }
    }
}
