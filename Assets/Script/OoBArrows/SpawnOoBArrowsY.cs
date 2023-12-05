using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOoBArrowsY : SpawnOoBArrows
{


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (InCornerPusher())
        {
            ResetArrowY(other);
            return;
        }


        SetArrowY(other);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        ResetArrowY(other); 
    }
}
