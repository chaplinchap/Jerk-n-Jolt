using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoBArrowY : MonoBehaviour
{

    //[SerializeField]private SpawnOoBArrows spawner;
    [SerializeField] private Rigidbody2D rb;

    private ArrowManager arrowManager;


    private void Start() 
    {
        
    }


    private void Update()
    {
        gameObject.transform.position = new Vector3(rb.transform.position.x, transform.position.y, transform.position.z);
    }

}
