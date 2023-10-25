using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player1PassThrough : MonoBehaviour
{

    public BoxCollider2D col;
    public float time = 0.15f;

    // Update is called once per frame
    void Update()
    {
        //When pressing down the button "S" you lose your collision for a short while
        if (Input.GetKeyDown(KeyCode.S)) //Input
        {
            StartCoroutine(waittime()); //Activate class
            col.enabled = false;        //Disable collision
        }
    }
    
    IEnumerator waittime()                      //Happeens at the same time as the if statement
    {
        yield return new WaitForSeconds(time);  //Given wait time in seconds
        col.enabled = true;                     //After countdown enable collision again
    } 
}




