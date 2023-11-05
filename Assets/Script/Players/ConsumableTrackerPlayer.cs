using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ConsumableTrackerPlayer : MonoBehaviour
{
    public GameObject consumbalbeSpawner;

    private float time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Consumables"))

        {
             collision.gameObject.GetComponent<Collider2D>().enabled = false;
             collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

             StartCoroutine(detractFromCurrentConsumables(time));
            //consumbalbeSpawner.GetComponentInChildren<ConsumV4>().currentConsumablesTracker--;
           // collision.gameObject.SetActive(false);
        }
    }


    public IEnumerator detractFromCurrentConsumables(float time)
    {
        yield return new WaitForSeconds(time);
        consumbalbeSpawner.GetComponent<ConsumV4>().currentConsumablesTracker--;
        
           

    }
}
