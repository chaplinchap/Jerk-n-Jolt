using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class ConsumV4 : MonoBehaviour
{
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public List<Rect> spawnAreas = new List<Rect>();
    public GameObject suddenDeathObject;
    private DeathGameChange suddenDeathScript;
    public float minSpawnDelay = 2f;  // Change as needed
    public float maxSpawnDelay = 5f;  // Change as needed
    private int maxSpawnAttempts = 250; // Maximum attempts to find a valid position
    private float minDistanceBetweenItems = 1f; // Distance between itemPrefabs

    public int maxConsumables = 3;
    public static int currentConsumablesTracker = 0;

 
    
    

    private float nextSpawnTime;
    [SerializeField] public List<Vector2> spawnedItemPositions = new List<Vector2>();


    private void Awake()
    {
        currentConsumablesTracker = 0;
    }


    void Start()
    {
        // consumableParent = consumbleParentObject.GetComponent<ConsumableParentObject>();
        nextSpawnTime = GetRandomSpawnTime();
       suddenDeathScript = suddenDeathObject.GetComponent<DeathGameChange>();
    }

    void Update()
    {


        if (Time.time >= nextSpawnTime && currentConsumablesTracker < maxConsumables && !suddenDeathScript.suddenDeathTriggered)
        {
           

            // Choose a random spawn area and item prefab
            int randomSpawnAreaIndex = Random.Range(0, spawnAreas.Count);
            int randomItemPrefabIndex = Random.Range(0, itemPrefabs.Count);

            // Get a random position within the selected spawn area
            Rect selectedSpawnArea = spawnAreas[randomSpawnAreaIndex]; // Work with the selected Area
            Rect area = selectedSpawnArea; //Simplification

            Vector2 randomPosition = Vector2.zero;
            bool validPosition = false;
            int attempts = 0;

            while (attempts < maxSpawnAttempts)
            {
                // Get a random position
                randomPosition = new Vector2(
                    Random.Range(area.x, area.x + area.width),
                    Random.Range(area.y, area.y + area.height)
                );

                if (IsPositionValid(randomPosition))
                {
                    validPosition = true;
                    break;
                }

                attempts++;
            }

            if (!validPosition)
            {
                Debug.LogWarning("No valid spawn positions available in the selected area after " + maxSpawnAttempts + " attempts.");
                return;
            }

            // Keep track of spawned objects positions. Used for checking for no collisions in the future
            spawnedItemPositions.Add(randomPosition);

            // Instantiate the selected item prefab at the random position
            GameObject selectedItemPrefab = itemPrefabs[randomItemPrefabIndex];
            GameObject spawnObject = Instantiate(selectedItemPrefab, randomPosition, Quaternion.identity);
            
              

            currentConsumablesTracker++;
            



            // Set the next spawn time
            nextSpawnTime = Time.time + GetRandomSpawnTime();
        }
    }

    float GetRandomSpawnTime()
    {
        return Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 itemPosition in spawnedItemPositions)
        {
            float distance = Vector2.Distance(position, itemPosition);
            if (distance < minDistanceBetweenItems)
            {
                return false;
            }
        }
        return true;
    }


    //for Debugging purpose. Shows area where objects can spawn
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var spawnArea in spawnAreas)
        {
            //Draw rectangular shape
            Vector3 min = new Vector3(spawnArea.x, spawnArea.y, 0);
            Vector3 max = new Vector3(spawnArea.x + spawnArea.width, spawnArea.y + spawnArea.height, 0);

            // Draw the bottom line of the rectangle
            Gizmos.DrawLine(min, new Vector3(max.x, min.y, 0));
        }
    }

}
