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

    public float startingPowerPush = 100;
    public float startingPowerPull = 35;

    public float startingMoveSpeedPush = 160;
    public float startingMoveSpeedPull = 160;

    public float startingFrictionPush = 0.5f;
    public float startingFrictionPull = 0.5f;


    public override void ApplyPusher(GameObject target)
    {
        rollDice = Random.Range(1, 4);
        Debug.Log("the dice roll was: " + rollDice);

        target.GetComponent<HealthV2>().GiveHP(hp);
        /*
        startingFrictionPush = target.GetComponent<Rigidbody2D>().sharedMaterial.friction;
        startingMoveSpeedPush = target.GetComponent<PlayerMovement>().speed;
        startingPowerPush = target.GetComponent<Push>().pushForce;
        */

        if (rollDice == 1)
        {
            target.GetComponent<Push>().pushForce /= amountPowerReduced;
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

    }

    public override void ApplyPuller(GameObject target)
    {
        rollDice = Random.Range(1, 4);

        target.GetComponent<HealthV2>().GiveHP(hp);
        /*
        startingPowerPull = target.GetComponent<Pull>().pullForce;
        startingMoveSpeedPull = target.GetComponent<PlayerMovement>().speed;
        startingFrictionPull = target.GetComponent<Rigidbody2D>().sharedMaterial.friction;
        */
        if (rollDice == 1)
        {
            target.GetComponent<Pull>().pullForce /= amountPowerReduced;
        }

        if (rollDice == 2)
        {
            target.GetComponent<PlayerMovement>().speed /= amountSpeedReduced;
        }

        if (rollDice == 3)
        { 
            target.GetComponent<Rigidbody2D>().sharedMaterial.friction /= amountFrictionReduced;
        }
    }

    public override void DeApplyPusher(GameObject target)
    {
            target.GetComponent<Push>().pushForce = startingPowerPush;      
            target.GetComponent<PlayerMovement>().speed = startingMoveSpeedPush;
            target.GetComponent<Rigidbody2D>().sharedMaterial.friction = startingFrictionPush;           

    }

    public override void DeApplyPuller(GameObject target)
    {
            target.GetComponent<Pull>().pullForce = startingPowerPull;
            target.GetComponent<PlayerMovement>().speed = startingMoveSpeedPull;
            target.GetComponent<Rigidbody2D>().sharedMaterial.friction = startingFrictionPull;
    }

}
