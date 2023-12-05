using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoBArrowCanvas : MonoBehaviour
{

    private ArrowManager arrowManager;
    private Rigidbody2D rb;
    //[SerializeField] private float speed = 2; 

    
    private RectTransform rectTransform;
    private RectTransform parentRect;

    private readonly float offSet = 50f;
    


    void Start()
    {

        arrowManager = GetComponentInParent<ArrowManager>();

        parentRect = arrowManager.GetParentRectTransform();
        rb = arrowManager.GetRB();

        rectTransform = GetComponent<RectTransform>();
       // parentRect = GetComponentInParent<RectTransform>();
        //offSet = transform.position.y;
    }



    protected void SetArrowHeight() 
    {
        float positionY = (parentRect.rect.height / Camera.main.orthographicSize)/2f * rb.transform.position.y + parentRect.rect.height/2f;


        //Debug.Log(positionY);
        //Debug.Log(parentRect.rect.height);


        if (positionY > parentRect.rect.height) 
        {
            rectTransform.transform.position = new Vector3(transform.position.x, parentRect.rect.height , transform.position.z);
            return;
        }

        rectTransform.transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
        //rectTransform.transform.position = new Vector3(transform.position.x, (speed * parentRect.rect.height - Camera.main.rect.height) * rb.transform.position.y + offSet, transform.position.z);
        //rectTransform.transform.position = new Vector3(transform.position.x,  rb.transform.position.y, transform.position.z);

    }

    protected void SetArrowWidth() 
    {
        float positionX = (parentRect.rect.width / Camera.main.orthographicSize)/4f * rb.transform.position.x + parentRect.rect.width/2f - offSet;


        if (positionX < -offSet)
        {
            rectTransform.transform.position = new Vector3(-offSet, transform.position.y, transform.position.z);
            return;
        }

        if (positionX > parentRect.rect.width - offSet)
        {
            rectTransform.transform.position = new Vector3(parentRect.rect.width - offSet, transform.position.y, transform.position.z);
            return;
        }
        

        rectTransform.transform.position = new Vector3(positionX, transform.position.y,  transform.position.z);
        //rectTransform.transform.position = new Vector3(transform.position.x, (speed * parentRect.rect.height - Camera.main.rect.height) * rb.transform.position.y + offSet, transform.position.z);
        //rectTransform.transform.position = new Vector3(transform.position.x,  rb.transform.position.y, transform.position.z);

    }
}
