using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer lr;
    private Transform[] points;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }


    private void Update()
    {

        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            lr.material.SetTexture("_MainTex", textures[animationStep]);
        

            fpsCounter = 0f;

        }



        for (int i = 0; i < points.Length; i++) {
            lr.SetPosition(i, points[i].position);
        }
    }


    public void SetUpLine(Transform[] points) { 
        lr.positionCount = points.Length;
        this.points = points;
    
    }


}
