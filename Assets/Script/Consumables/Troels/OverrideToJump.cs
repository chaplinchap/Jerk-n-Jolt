using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
[CreateAssetMenu(menuName = "Powerup/Jump")] // This will make a new menu folder under the Create folder such that it is easier to doublicate more of them
public class OverrideToJump : ConsumableScriptableObject    // This inherits from PowerUpEffect
{
    public float amount = 2f;

    private float startingJump;

    public override void Apply(GameObject target)
    {
        startingJump = target.GetComponent<PlayerMovement>().jumpingPower; // First there is a check if the GameObject that has collided with the trigger has a component
                                                                           // of type PlayerMovment. If it does then the power of the jump is stored in a variable
        target.GetComponent<PlayerMovement>().jumpingPower *= amount;   // Then it is multiplyed by the amount

    }

    public override void DeApply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().jumpingPower = startingJump; // The jumping power is assiged the startning value it had before it returned

    }

}
*/