using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is the parent class to all the powerup effects, to be a child of this means they have to inherit the two methods in this class.
 * When we initiaise the specific powerup we cast them up as a PowerUpEffect and in turn it will call upon the specific methods written in
 * the child class.
 */


public abstract class PowerUpEffect : ScriptableObject
{
    public abstract void Apply(GameObject target);

    public abstract void DeApply(GameObject target);
}
