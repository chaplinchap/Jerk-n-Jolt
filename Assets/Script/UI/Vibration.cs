using System.Collections;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    public float shakeAmount = 0f; 
    private float shakeSpeed = 1f;
    public bool coroutineRunning = false;

    Vector3 originalPosition;
    [SerializeField] private KeyCode abilityPressed = KeyCode.Space;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void LateUpdate()
    {        
        ShakeObject();

        if (Input.GetKeyDown(abilityPressed) && !coroutineRunning)
        {
            StartCoroutine (ShakeIntensity());
        }
        else if (Input.GetKeyUp(abilityPressed) && coroutineRunning)
        {
            StopAllCoroutines();
            coroutineRunning = false;
            shakeAmount = 0.0f;
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

    public IEnumerator ShakeIntensity()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(2);
        shakeAmount = 0.04f;
        yield return new WaitForSeconds(1);
        shakeAmount = 0.1f;
        yield return new WaitForSeconds(1);
        shakeAmount = 0.0f;
        coroutineRunning = false;
    }
}