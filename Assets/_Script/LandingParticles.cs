using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticles : MonoBehaviour
{
    public ParticleSystem landingParticles;
    public Rigidbody2D rb;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParticleTrigger"))
        {
            if (rb.velocity.y <= 0)
            {
                LandParticles();   
            }
        }
 
    }
    public void LandParticles()
    {
        landingParticles.Play();
    }

}