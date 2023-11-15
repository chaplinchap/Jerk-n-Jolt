using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStunner : Stunner
{
    private Push pushScript;

    public float stunTime;
    public float timeToStun;

    void Update()
    {
        Stun(timeToStun, stunTime, pushScript.pushOnPress);

    }




    protected override void GetScripts()
    {
        base.GetScripts();
        pushScript = GetComponent<Push>();
    }

    protected override void TurnScripts(bool turn)
    {
        base.TurnScripts(turn);
        pushScript.enabled = turn;
    }
}
