using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformScriptableObject : ScriptableObject
{


    public abstract void Fade(GameObject target);

    public abstract void DeFade(GameObject target);

    public abstract void Despawn(GameObject target);

    public abstract void Spawn(GameObject target);

}
