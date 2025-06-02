using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    AudioManager audioManager;

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ButtonSFXSound()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }
}
