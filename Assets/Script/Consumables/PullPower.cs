using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerup/PullPower")]
public class PullPower : PowerUpEffect
{
    public float amount = 2f;
    private float startForce;


    public override void Apply(GameObject target)
    {
        startForce = target.GetComponent<Pull>().extraForce;
        target.GetComponent<Pull>().extraForce *= amount;
    }

    public override void DeApply(GameObject target)
    {
        target.GetComponent<Pull>().extraForce = startForce;
    }

}
