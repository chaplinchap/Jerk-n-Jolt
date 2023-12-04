using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoBArrowX : MonoBehaviour
{


    //[SerializeField]private SpawnOoBArrows spawner;
    [SerializeField] private Rigidbody2D rb;

    private ArrowManager arrowManager;

    private void Start() 
    {
        
    
    }

    private void Update() 
    {
            gameObject.transform.position = new Vector3(transform.position.x, rb.transform.position.y, transform.position.z); 
    }


}
