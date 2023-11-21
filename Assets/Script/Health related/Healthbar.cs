using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    int ImageLength = 12; //Image length has 12 hearts
    [SerializeField] private HealthV2 playerHealth; //Input player health
    [SerializeField] private Image totalHealthbar; //UI of healthbar (background)
    [SerializeField] private Image currentHealthbar; //UI of players current healthbar (foreground)

    private bool startShake;
    public bool permanentShake;
    public float shakeAmount = 10f; 
    private float shakeSpeed = 0.1f;
    Vector3 originalPosition;

    private void Start()
    {
        //Access to image fill amount. players starting health (3) devided by ImageLenght (12) 
        totalHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
        originalPosition = transform.position;

    }

    private void Update()
    {
        //Access to image fill amount. look for what player health is. when losing health change fill amount
        currentHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;

        if (startShake)
        {
            StartCoroutine(ShakeShake());
            
        }

        if (playerHealth.currentHealth == 1)
        {
            shakeAmount = 2f;
            StartCoroutine(ShakeShake());
            permanentShake = true;           
        }

        if (permanentShake)
            {
                permanentShake = false;
                shakeAmount = 10f;
            }
    }

    public void ShakeObject()
    {
        startShake = true;        
    }

    IEnumerator ShakeShake()
    {
        // Generate random offsets for each axis
        float offsetX = Random.Range(-shakeAmount, shakeAmount);
        float offsetY = Random.Range(-shakeAmount, shakeAmount);
        float offsetZ = Random.Range(-shakeAmount, shakeAmount);

        // Calculate a wiggling effect
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // Apply the random offsets to the targets position
        transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ) + new Vector3(shake, 0f, 0f);
        Debug.Log("Shake!!!!");
        yield return new WaitForSeconds(1);
        startShake = false;
        if (!permanentShake)
        {
            transform.position = originalPosition;
        }
        
    }
}
