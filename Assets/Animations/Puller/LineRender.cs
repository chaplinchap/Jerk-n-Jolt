using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

    [SerializeField] private float lineDuation;

    private LineRenderer lineRenderer;

    private Pull pullScript;

    Vector3[] vectors = new Vector3[2];

    private Transform target;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Pull pullScript = GetComponentInParent<Pull>();


        lineRenderer.positionCount = 2;

        vectors[0] = GetComponentInParent<Transform>().position;

        Debug.Log(vectors[0]);

        vectors[1] = pullScript.VectorBetween();
        

        StartCoroutine(AnimateLine());
        //StopCoroutine(AnimateLine());
    }

    
    void Update()
    {
        lineRenderer.SetPosition(1,target.position);


    }

    public void AssignTarget(Vector3 startingPosition, Transform newTarget) 
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startingPosition);

        target = newTarget;



    }


    private IEnumerator AnimateLine() 
    {
        float startTime = Time.time;


        //lineRenderer.SetPosition(0, new Vector3(10,10,0));
        

        Vector3 startPosition = lineRenderer.GetPosition(0);
        Vector3 endPosition = lineRenderer.GetPosition(1);

        Vector3 position = startPosition;

        while (position != endPosition) 
        {
            float time = (Time.time - startTime) / lineDuation;
            position = Vector3.Lerp(startPosition, endPosition, time);
            lineRenderer.SetPosition(1, position);
            
            yield return null;
        }





    }

}
