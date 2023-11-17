using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeFeedbackLoop : MonoBehaviour
{

    public float increaseMultiplier = 2f; 

    private HealthV2 healthScript;
    private AbilityPower abilityPowerScript;

    private float startingForce;
    private float startingHealth;

    void Start()
    {
        healthScript = GetComponent<HealthV2>();
        
        abilityPowerScript = GetComponent<AbilityPower>();
        
        startingForce = GetForce();
        startingHealth = GetHealth();
    }

    void Update()
    {
        if (startingHealth != GetHealth()) {

            //CameraShake.Instance.ShakeCamera(10f, .5f);

            SetForce();
            startingHealth = GetHealth();
        }
    }


    private float GetHealth()
    { return healthScript.currentHealth; }


    public float GetForce() 
    {
        return abilityPowerScript.abilityPowerForce;
    }


    private void SetForce()
    {
        float force = 1;

        if (GetHealth() == 3)
        {

            force = Mathf.Floor(startingForce);

        }
        else if (GetHealth() == 2)
        {

            force = Mathf.Floor(startingForce * increaseMultiplier);

        }
        else if (GetHealth() == 1) {

            force = Mathf.Floor(startingForce * increaseMultiplier * increaseMultiplier);
        
        }


        abilityPowerScript.abilityPowerForce = force;
            
    }


}
