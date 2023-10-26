using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public abstract void Apply(GameObject target);

    public abstract void DeApply(GameObject target);
}
