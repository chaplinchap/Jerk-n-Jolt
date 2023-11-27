using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeSuddenDeath : MonoBehaviour
{

    public static CameraShakeSuddenDeath Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private float startingIntensity;
    private float shakeTime;
    private float shakeTimeTotal;

    [SerializeField] private bool isSuddenDeath = false;

    private void Awake()
    {

        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    }


    private void Update()
    {
        StopShake();
    }


    public void ShakeCamera(float intensity, float time)
    {

        StartCoroutine(SuddenDeathTimer(time - 1f));

        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTime = time;
        shakeTimeTotal = time;
    }

    private void StopShake()
    {

        if (shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;

            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTime / shakeTimeTotal));

        }

    }

    private IEnumerator SuddenDeathTimer(float duration) 
    {

        isSuddenDeath = true;
        yield return new WaitForSeconds(duration);   
        isSuddenDeath = false;
    }


    public bool IsSuddenDeath() { return isSuddenDeath; }
}
