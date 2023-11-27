using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformScriptableObject : ScriptableObject
{

    public abstract void CancelDespawn(GameObject target);

    public abstract void Fade(GameObject target);

    public abstract void Despawn(GameObject target);

    public abstract void Spawn(GameObject target);

    public abstract void Destroy(GameObject target);

    public abstract void ChangeColor(GameObject target);

}
