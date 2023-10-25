using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpParent : MonoBehaviour
{
    //  METHODS \\ 

    public IEnumerator Duration(PowerUpEffect power, GameObject player, float time)
    {
        // Debug.Log("Working here");
        yield return new WaitForSeconds(time);
        // Debug.Log("Working"); 
        power.DeApply(player);
        Destroy(gameObject);
    }

    public void TurnOffConsumable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
