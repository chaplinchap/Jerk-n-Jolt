using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Area : MonoBehaviour
{
    public GameObject[] ItemPrefab; //Choose which object you want to spawn
    public float Radius = 1f;
     public Vector3 center; // Decide location of spawn area
    public Vector3 size; // Decide how big spawn area is

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 randomPos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2), 
            Random.Range(-size.y / 2, size.y /2), 
            Random.Range(-size.z / 2, size.z / 2));

        GameObject gameObject = Instantiate(ItemPrefab[Random.Range(0, ItemPrefab.Length)], randomPos, Quaternion.identity);
    }
}
