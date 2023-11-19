using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerup/Heart")]
public class OverrideToHeart : ConsumableScriptableObject
{

    [SerializeField] private float hp = 1;
    private int rollDice;

    private float amountPowerReduced = 2;
    private float amountSpeedReduced = 1.4f;
    private float amountFrictionReduced = 20;

    public float startingPower;    
    public float startingMoveSpeed;
    public float startingFriction;

    

    public override void Apply(GameObject target)
    {
        /*
        rollDice = Random.Range(1, 4);
        Debug.Log("the dice roll was: " + rollDice);
        */
        
        target.GetComponent<HealthV2>().GiveHP(hp);
        
        // startingFriction = target.GetComponent<Rigidbody2D>().sharedMaterial.friction;
        // startingMoveSpeed = target.GetComponent<PlayerMovement>().speed;
        startingPower = target.GetComponent<AbilityPower>().abilityPowerForce;

        target.GetComponent<AbilityPower>().abilityPowerForce /= amountPowerReduced;

        /*
        if (rollDice == 1)
        {
            target.GetComponent<Push>().abilityPowerForce /= amountPowerReduced;
            Debug.Log("Ability force was reduced");
        }

        if(rollDice == 2)
        {
            target.GetComponent<PlayerMovement>().speed /= amountSpeedReduced;
            Debug.Log("Speed was reduced");
        }

        if(rollDice == 3)
        {
            target.GetComponent<Rigidbody2D>().sharedMaterial.friction /= amountFrictionReduced;
            Debug.Log("Friction was reduced");
        }
        */

    }

   
    public override void DeApply(GameObject target)
    {
            target.GetComponent<AbilityPower>().abilityPowerForce = startingPower;      
            // target.GetComponent<PlayerMovement>().speed = startingMoveSpeed;
            // target.GetComponent<Rigidbody2D>().sharedMaterial.friction = startingFriction;           

    }

 
}
