using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStunner : Stunner
{

    private Pull pullScript;

    void Update() 
    {
        Stun(timeToStun, stunTime, pullScript.pullOnPress);
        stunbarScript.UpdateStunBar(GetTime(), timeToStun);
    }




    protected override void GetScripts()
    {
        base.GetScripts();
        pullScript = GetComponent<Pull> ();
    }

}
