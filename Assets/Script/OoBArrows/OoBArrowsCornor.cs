using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OoBArrowsCornor : MonoBehaviour
{

    [SerializeField] private SpawnOoBArrowsX SpawnOoBArrowsXLeft;
    [SerializeField] private SpawnOoBArrowsX SpawnOoBArrowsXRight;
    [SerializeField] private SpawnOoBArrowsY SpawnOoBArrowsY;
    private Image[] cornerArrows;



    void Start()
    {
        //ooBArrowsXLeft = GetComponent<SpawnOoBArrowsX>();
        //ooBArrowsXRight = GetComponent<SpawnOoBArrowsX>();
        //ooBArrowsY = GetComponent<SpawnOoBArrowsY>();

        cornerArrows = GetComponentsInChildren<Image>();


        for (int i = 0; i < cornerArrows.Length; i++)
        {
            cornerArrows[i].enabled = false;
        }

        
    }



    void Update()
    {

        

        

        
    }


    private bool InCornerRightPush() 
    {

        if (SpawnOoBArrowsXRight.IsOOBPusherX() && SpawnOoBArrowsY.IsOOBPusherY())
        {
            return true;    
        
        }
        else { return false; }

    }
    
    private bool InCornerLeftPush() 
    {

        if (SpawnOoBArrowsXLeft.IsOOBPusherX() && SpawnOoBArrowsY.IsOOBPusherY())
        {
            return true;    
        
        }
        else { return false; }

    }   
    
    private bool InCornerRightPull() 
    {

        if (SpawnOoBArrowsXRight.IsOOBPullerX() && SpawnOoBArrowsY.IsOOBPullerY())
        {
            return true;    
        
        }
        else { return false; }

    }
    
    private bool InCornerLeftPull() 
    {

        if (SpawnOoBArrowsXLeft.IsOOBPullerX() && SpawnOoBArrowsY.IsOOBPullerY())
        {
            return true;    
        
        }
        else { return false; }

    }


    protected void ArrowCornerPusher() 
    {
        if (InCornerRightPush())
        {
            cornerArrows[0].enabled = true;
        }
        else
            cornerArrows[0].enabled = false;


        if (InCornerLeftPush())
        {
            cornerArrows[1].enabled = true;
        }
        else
            cornerArrows[1].enabled = false;
    }   
    
    protected void ArrowCornerPuller() 
    {
        if (InCornerRightPull())
        {
            cornerArrows[0].enabled = true;
        }
        else
            cornerArrows[0].enabled = false;


        if (InCornerLeftPull())
        {
            cornerArrows[1].enabled = true;
        }
        else
            cornerArrows[1].enabled = false;
    }

}
