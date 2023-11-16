using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStunner : Stunner
{
    private Push pushScript;

    void Update()
    {
        Stun(timeToStun, stunTime, pushScript.pushOnPress);
        stunbarScript.UpdateStunBar(GetTime(), timeToStun);
    }




    protected override void GetScripts()
    {
        base.GetScripts();
        pushScript = GetComponent<Push>();
    }

}
