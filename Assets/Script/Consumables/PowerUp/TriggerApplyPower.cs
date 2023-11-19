using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerApplyPower : ConsumableParentObject
{

    public float time = 5f; // Duration of the power up.  

    public ConsumableScriptableObject powerUp;
    private bool triggerOnce = false;
    

    private float timeStampOnAwake;
    private float timeToDespawn = 5f;
    private float timeCoroutineDespawn = 3f;
    private bool isDespawned = false;

    private void Awake()
    {
        timeStampOnAwake = Time.time;
        
    }

    

    private void Update()
    {
        if(Time.time - timeStampOnAwake  > timeToDespawn && !isDespawned) 
        {
            isDespawned = true;
            StartCoroutine(DespawnConsumable(timeCoroutineDespawn));
        
        }
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Pusher") && !triggerOnce || collision.CompareTag("Puller") && !triggerOnce)

        {
            triggerOnce = true;
             
           TurnOffConsumable();
           StartCoroutine(Buff(collision.gameObject, time));
                
        }

    }


    private IEnumerator Buff (GameObject player, float time)
    {
        powerUp.Apply(player);
        yield return new WaitForSeconds(time);
        powerUp.DeApply(player);
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
