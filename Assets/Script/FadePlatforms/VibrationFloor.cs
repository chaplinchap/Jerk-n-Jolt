using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class VibrationFloor : MonoBehaviour
{
    public float shakeAmount = 0f; 
    private float shakeSpeed = 1f;
    private ParticleSystem particleSystem;
    private AudioManager audioManager;
    

    Vector3 originalPosition;
  

    void Start()
    {
        originalPosition = transform.localPosition;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
            audioManager.StartFloorShake();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
            collision.transform.SetParent(transform);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.interpolation = RigidbodyInterpolation2D.None;
            ShakeObject();

            try
            {
                particleSystem.Play();
            }
            catch { }

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Puller") || collision.gameObject.CompareTag("Pusher"))
        {
            //audioManager.StopFloorShake();
            collision.transform.SetParent(null);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;

            try
            {
                particleSystem.Stop();
            }
            catch {}

           // shakeAmount = 0f;
        }
    }

    private void ShakeObject()
    {
        // Generate random offsets for each axis
        float offsetX = Random.Range(-shakeAmount, shakeAmount);
        float offsetY = Random.Range(-shakeAmount, shakeAmount);
        float offsetZ = Random.Range(-shakeAmount, shakeAmount);

        // Calculate a wiggling effect
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // Apply the random offsets to the targets position
         transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, offsetZ) + new Vector3(shake, 0f, 0f);
    }

    

}