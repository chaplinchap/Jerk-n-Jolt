using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathVibrationOnDestroyed : MonoBehaviour
{
    public float shakeAmount = 1.5f;
    private float shakeSpeed = 1.25f;

    Vector3 originalPosition;



    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(DeathGameChange.suddenDeathTriggered)
        {
            ShakeObject();
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
