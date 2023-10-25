using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerup/PushPower")] 
public class PushPower : PowerUpEffect
{
    public float amount = 2f;
    private float startForce;


    public override void Apply(GameObject target) 
    {
        startForce = target.GetComponent<Push>().extraForce;
        target.GetComponent<Push>().extraForce *= amount;
    }

    public override void DeApply(GameObject target) 
    { 
        target.GetComponent<Push>().extraForce = startForce;
    }

}
