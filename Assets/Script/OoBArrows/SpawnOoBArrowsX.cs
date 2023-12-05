using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOoBArrowsX : SpawnOoBArrows
{


    private void OnTriggerEnter2D(Collider2D other) 
    {


        SetArrowX(other);
    
        if (InCornerPusher()){
            ResetArrowX(other);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ResetArrowX(other);

    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        SetArrowCorner(other);
    }


}
