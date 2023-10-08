using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player2PassThrough : MonoBehaviour
{

    public BoxCollider2D col;
    public float time = 0.15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(waittime());
            col.enabled = false;            
        }
    }

    IEnumerator waittime()
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
    }
}