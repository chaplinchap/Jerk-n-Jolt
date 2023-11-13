using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeFeedbackLoop : MonoBehaviour
{

    public float decreaseMultiplier = 2f; 

    private HealthV2 healthScript;
    private Pull pullScript;
    private Push pushScript;

    private float startingForce;
    private float startingHealth;

    void Start()
    {
        healthScript = GetComponent<HealthV2>();


        try
        {
            pullScript = GetComponent<Pull>();
        }
        catch { }

        try
        {
            pushScript = GetComponent<Push>();
        }
        catch { }

        startingForce = GetForce();
        startingHealth = GetHealth();
    }

    void Update()
    {
        if (startingHealth != GetHealth()) {

            CameraShake.Instance.ShakeCamera(10f, .5f);

            SetForce();
            startingHealth = GetHealth();
        }
    }


    private float GetHealth()
    { return healthScript.currentHealth; }


    private float GetForce() 
    {
        float force = 1;
        try
        {
            force = pullScript.pullForce;
            //Debug.Log("It works "+force);
        }
        catch { }

        try {
            force = pushScript.pushForce;
            //Debug.Log("Nice try Catch");
        }
        catch { }

        return force;
    }


    private void SetForce()
    {
        float force = 1;

        if (GetHealth() == 3)
        {

            force = startingForce;

        }
        else if (GetHealth() == 2)
        {

            force = startingForce / decreaseMultiplier;

        }
        else if (GetHealth() == 1) { 
            
            force = startingForce / decreaseMultiplier / decreaseMultiplier;
        
        }


        try
        {
            pullScript.pullForce = force;
            //Debug.Log("It works "+force);
        }
        catch { }

        try
        {
            pushScript.pushForce = force;
            //Debug.Log("Nice try Catch");
        }
        catch { }

    }


}
