using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathBackground : MonoBehaviour
{

    private ParticleSystem.EmissionModule damageTriggerParticleSystem;
    private ParticleSystem damageTriggerParticleSystemSpeed;

    

    private bool isTriggeredOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if(DeathGameChange.suddenDeathTriggered && !isTriggeredOnce)
        {
            isTriggeredOnce = true;
            StartCoroutine(EmissionParticles());
        }
    }

    // Update is called once per frame
    IEnumerator EmissionParticles()
    {
            for (int i = 60; i < 100; i=+5)
            {
                // damageTriggerParticleSystemSpeed.start
                damageTriggerParticleSystem.rateOverTime = i;
                yield return new WaitForSeconds(0.25f);
            }
        
    }
}
