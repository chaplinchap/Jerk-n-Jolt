using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticles : MonoBehaviour
{
    public ParticleSystem landingParticles;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            LandParticles();
        }
    }
    void LandParticles()
    {
        landingParticles.Play();
    }

}
