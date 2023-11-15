using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStunner : Stunner
{

    private Pull pullScript;

    public float stunTime;
    public float timeToStun;

    void Update() 
    {
        Stun(timeToStun, stunTime, pullScript.pullOnPress);
    
    }




    protected override void GetScripts()
    {
        base.GetScripts();
        pullScript = GetComponent<Pull> ();
    }

    protected override void TurnScripts(bool turn)
    {
        base.TurnScripts(turn);
        pullScript.enabled = turn;
    }



}
