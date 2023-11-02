using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpParent : MonoBehaviour
{

    /**
     * This class exists such that you do not have to make the Coroutine and TurnOffConsumable Methodes
     * in the power up triggers. They should inherit this class to make it easier to understand
     * and make powerups.
     */

    //  METHODS \\ 


    /**
     * The coroutine Duration takes three inputs, an object of type PowerUpEffect, an object of type GameObject and a float.
     * 
     * The method is here to make the undo the effects of powerups after a given time.
     * 
     * The PowerUpEffect is an abstract class that has two Methods which are used to Apply and DeApply the powerup.
     * For more infomation on the class go to the PowerUpEffect to read more.
     */

    public IEnumerator Duration(PowerUpEffect power, GameObject player, float time)
    {
        yield return new WaitForSeconds(time);  // This will yield for the duration of the powerup 
        power.DeApply(player);  // This will DeAppley the powerup from the Player which has taken it
        Destroy(gameObject); // This destroyes the Object that holds the powerup, such that is does not clog up in memory
    }



    /**
     * The method TurnOffConsumable simply turns off the Sprite and Collider of the object that holds the Consumable.
     * It exsist because if you were to destory the gameObject op front the DeApply method cannot be called.
     * Which is why the Destory(gameObject) first happens in the Coroutine.
     */

    public void TurnOffConsumable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
