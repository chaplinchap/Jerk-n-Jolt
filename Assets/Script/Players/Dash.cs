using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MovementAid
{
    protected override void Update()
    {
        base.Update();
        Dashing();
    }
}
