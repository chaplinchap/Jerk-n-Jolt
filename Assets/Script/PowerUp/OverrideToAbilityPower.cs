using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Powerup/Power")]
public class OverrideToAbilityPower : ConsumableScriptableObject
// Start is called before the first frame update
{
    public float startingPowerPush = 100;
    public float startingPowerPull = 35;

    public float amountPower = 2;

    public override void ApplyPusher(GameObject target)
    {

        // startingPowerPush = target.GetComponent<Push>().pushForce;
        target.GetComponent<Push>().pushForce *= amountPower;

        
    }

    public override void ApplyPuller(GameObject target)
    {
        //startingPowerPull = target.GetComponent<Pull>().pullForce;
        target.GetComponent<Pull>().pullForce *= amountPower;
    }

    public override void DeApplyPusher(GameObject target)
    {
        target.GetComponent<Push>().pushForce = startingPowerPush;
    }

    public override void DeApplyPuller(GameObject target)
    {
        target.GetComponent<Pull>().pullForce = startingPowerPull;
    }
}

