using Unity.VisualScripting;
using UnityEngine;

public class ConsumV3 : MonoBehaviour
{
    public static Area Instance;

    public GameObject[] ItemPrefab; //Choose which object you want to spawn
    public Transform[] SpawnLocations; //Chose which area for object to spawn

    //public BoxCollider2D collider;

    private float timer;
    private float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke ("SpawnObjectAtRandom", delay);
    }

    public void SpawnObjectAtRandom(Collider2D TestAreaCollider, GameObject[] prefab)
    {    
        Instantiate(ItemPrefab[Random.Range(0, ItemPrefab.Length)], SpawnLocations[Random.Range(0, SpawnLocations.Length)]);
        timer = Random.Range(15f, 30f);
        Debug.Log("Spawn time: {timer}");
        Invoke ("SpawnObjectAtRandom", timer);
    }


    private Vector2 GetRandomSpawnPosition(Collider2D TestAreaCollider)
    {
        Vector2 SpawnPosition = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int maxAttempts = 200;

        int LayerToNotSpawnOn = LayerMask.NameToLayer("wall");

        while (!isSpawnPosValid && attemptCount < maxAttempts)
        {
            SpawnPosition = GetRandomPointInCollider(TestAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(SpawnPosition, 2f);

        bool isInvalidCollision = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerToNotSpawnOn)
            {
                isInvalidCollision = true;
                break;
            }
        }

        if(isInvalidCollision)
        {
            isSpawnPosValid = true;
        }
        attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            Debug.LogWarning("Could not find a valid Spawn Position");
        }
        
        return SpawnPosition;
    }


    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        Bounds collBounds = collider.bounds;

        Vector2 minBounds = new Vector2(collBounds.min.x, collBounds.min.y);
        Vector2 maxBounds = new Vector2(collBounds.max.x, collBounds.max.y);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}