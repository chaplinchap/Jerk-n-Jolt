using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Powerup/Power")]
public class OverrideToAbilityPower : ConsumableScriptableObject
// Start is called before the first frame update
{
    
    public float startingPower;
    public float amountPower = 2;



    public override void Apply(GameObject target)
    {
        startingPower = target.GetComponent<AbilityPower>().abilityPowerForce;
        target.GetComponent<AbilityPower>().abilityPowerForce *= amountPower;
    }

 

    public override void DeApply(GameObject target)
    {
         // target.GetComponent<AbilityPower>().abilityPowerForce = startingPower;
         target.GetComponent<AbilityPower>().abilityPowerForce /= amountPower;
    }

}

