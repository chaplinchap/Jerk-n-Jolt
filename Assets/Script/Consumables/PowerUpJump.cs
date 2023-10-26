using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerup/Jump")]
public class PowerUpJump : PowerUpEffect
{
    public float amount = 2f;

    private float startingJump;

    public override void Apply(GameObject target)
    {
        startingJump = target.GetComponent<PlayerMovement>().jumpingPower;
        target.GetComponent<PlayerMovement>().jumpingPower *= amount;

    }

    public override void DeApply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().jumpingPower = startingJump;

    }

}
