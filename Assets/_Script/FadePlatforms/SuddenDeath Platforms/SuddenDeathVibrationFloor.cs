using System.Collections;
using UnityEngine;

public class SuddenDeathVibrationFloor : MonoBehaviour
{
    public float shakeAmount = 0f; 
    private float shakeSpeed = 1f;
    private ParticleSystem particleSystem;
    private AudioManager audioManager;
    private bool audioPlaying = false;

    Vector3 originalPosition;
  

    void Start()
    {
        originalPosition = transform.localPosition;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Puller") && DeathGameChange.suddenDeathTriggered || collision.gameObject.CompareTag("Pusher") && DeathGameChange.suddenDeathTriggered)
        {
            collision.transform.SetParent(transform);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.interpolation = RigidbodyInterpolation2D.None;

            shakeAmount = 0.25f;
            ShakeObject();
            if (!audioPlaying)
            {
                audioPlaying = true;
                audioManager.StartFloorShake();
            }
            
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Puller") && DeathGameChange.suddenDeathTriggered || collision.gameObject.CompareTag("Pusher") && DeathGameChange.suddenDeathTriggered)
        {
            collision.transform.SetParent(null);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
            audioPlaying = false;
            audioManager.StopFloorShake();

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