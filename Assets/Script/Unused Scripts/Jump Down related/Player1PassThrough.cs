using System.Collections;
using UnityEngine;

public class Player1PassThrough : MonoBehaviour
{
    //Variables
    private GameObject player;
    public float time = 0.15f;
    
    //Called at the start
    private void Start()
    {
        player = GameObject.FindWithTag("Pusher");  //Finds the object with given tag
    }

    // Update is called once per frame
    void Update()
    {
        //When pressing down the button "S" you lose your collision for a short while
        if (Input.GetKeyDown(KeyCode.S))    //Input
        {
            StartCoroutine(Waittime());                                             //Activate Coroutine
            player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");     //Disable collision
        }
    }
    
    IEnumerator Waittime()                                                  //Corutine will give feedback after given time
    {
        yield return new WaitForSeconds(time);                              //Given wait time in seconds
        player.gameObject.layer = LayerMask.NameToLayer("Default");         //After countdown enable collision again
    } 
}