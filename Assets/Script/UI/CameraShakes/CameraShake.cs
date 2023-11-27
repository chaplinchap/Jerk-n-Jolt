using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private float startingIntensity;
    private float shakeTime;
    private float shakeTimeTotal;



    private void Awake() { 

        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    }


    private void Update()
    {
        StopShake();
    }


    public void ShakeCamera(float intensity, float time) 
    {

        if (CameraShakeSuddenDeath.Instance.IsSuddenDeath()) return;

        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTime = time;
        shakeTimeTotal = time;
    }

    private void StopShake() {

        if (shakeTime > 0f) {
            shakeTime -= Time.deltaTime;

                cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1-(shakeTime / shakeTimeTotal));

        }
    
    }

}
