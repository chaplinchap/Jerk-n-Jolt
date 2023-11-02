using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerup/Speed")]
public class SpeedUp : PowerUpEffect
{
    public float amount = 2f;

    private float startingSpeed; 

    public override void Apply(GameObject target)
    {
        startingSpeed = target.GetComponent<PlayerMovement>().speed;
        target.GetComponent<PlayerMovement>().speed *= amount;
            
    }

    public override void DeApply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().speed = startingSpeed;

    }

}
