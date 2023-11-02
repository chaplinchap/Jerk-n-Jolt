using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Check the PowerUpJump for a specific comments.
 */


[CreateAssetMenu(menuName = "Powerup/Speed")]
public class SpeedUp : PowerUpEffect
{
    public float amount = 2f;

    private float startingSpeed; 

    public override void Apply(GameObject target)
    {
        startingSpeed = target.GetComponent<PlayerMovement>().speed; // Store staring speed
        target.GetComponent<PlayerMovement>().speed *= amount; // Multiply by amount
            
    }

    public override void DeApply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().speed = startingSpeed; // return staring speed to the original value

    }

}
