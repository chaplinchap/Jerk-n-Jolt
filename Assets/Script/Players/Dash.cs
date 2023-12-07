using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : MovementAid
{
    protected override void Update()
    {
        base.Update();

        if (dashButton != KeyCode.None) 
        {
            ToggleDash();
        }
        else if(dashButton == KeyCode.None) {

            Dashing();
        }
    }
}
