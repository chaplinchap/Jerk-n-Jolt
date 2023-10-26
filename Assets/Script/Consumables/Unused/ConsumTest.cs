using UnityEngine;

public class ConsumTest : MonoBehaviour
{
    public GameObject[] ItemPrefab; //Choose which object you want to spawn
    public Transform[] SpawnLocations; //Chose which area for object to spawn
    //public Vector3 center; // Decide location of spawn area
    //public Vector3 size; // Decide how big spawn area is

    private float timer;
    private float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke ("SpawnObjectAtRandom", delay);
    }

    void SpawnObjectAtRandom()
    {
        
        SpawnArea();


        timer = Random.Range(15f, 30f);
        Debug.Log("Spawn time: {timer}");
        Invoke ("SpawnObjectAtRandom", timer);
    }

    void SpawnArea()
   {
        //Vector3 randomPos = new Vector3(
            //Random.Range(-size.x / 2, size.x / 2), 
            //Random.Range(-size.y / 2, size.y /2), 
            //Random.Range(-size.z / 2, size.z / 2));


        //GameObject gameObject = Instantiate(ItemPrefab[Random.Range(0, ItemPrefab.Length)], randomPos, Quaternion.identity);
        Instantiate(ItemPrefab[Random.Range(0, ItemPrefab.Length)], SpawnLocations[Random.Range(0, SpawnLocations.Length)]);
    }
 
       /* public Vector2 SpawnLocations(Bounds bounds)
        {
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));

            float minx = bounds.size.x * -0.5f;
            float miny = bounds.size.y * -0.5f;

            return (Vector2) this.gameObject.transform.TransformPoint(
                new Vector2(Random.Range (minx, miny),
                Random.Range (miny, -miny)));
        } */

    //for Debugging purpose. Shows area where objects can spawn
    /*private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawCube(center, size);
    }*/
}