using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator circleWipe;
    private float transitionTime = 1f;

    private GameObject activePage;

    //GameObjects to be part of the Page GameObject
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Credit;
    public GameObject Lore;
    private AudioManager audioManager;
    private MainMenuAudio mainMenuAudio;

    [SerializeField] private CanvasGroup keyBindMenu;

    private void Start()
    {
        // Initialize by setting the active page to the main menu.
        activePage = MainMenu;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        mainMenuAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<MainMenuAudio>();
    }

    public void TransitionToPage(GameObject newPage)
    {
        //When changing to a new GameObject that isnt active start coroutine
        if (newPage != activePage)
        {
            StartCoroutine(Transition(newPage));
        }
    }

    private bool mainScreen = true; // This will always know its the mainScreen since it starts on MainMenu

    IEnumerator Transition(GameObject newPage)
    {
        // If on mainScreen animate from right to left
        if (mainScreen)
        {
            circleWipe.SetTrigger("RightStart");
            yield return new WaitForSeconds(transitionTime);
            circleWipe.SetTrigger("RightEnd");
            mainScreen = false;
        }
        else if (!mainScreen) // if not on mainScreen animate from left to right
        {
            circleWipe.SetTrigger("LeftStart");
            yield return new WaitForSeconds(transitionTime);
            circleWipe.SetTrigger("LeftEnd");
            mainScreen = true;
        }

        activePage.SetActive(false); //Deactivate the current active page
        newPage.SetActive(true); //Activate the new page

        activePage = newPage; // the now active page is now considered the activePage
    }

    public void showKeyBinds()
    {
        keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
        keyBindMenu.blocksRaycasts = keyBindMenu.blocksRaycasts == true ? false : true;
    }

    public void Mute(bool mute)
    {
        if (mute)
        {
            /*audioManager.musicSource.volume = 0f;
            audioManager.SFXSource.volume = 0f;
            audioManager.countDownSource.volume = 0f;
            audioManager.suddenDeathMusicSource.volume = 0f;
            audioManager.stunSoundSource.volume = 0f;
            audioManager.floorShakeSource.volume = 0f;*/

            mainMenuAudio.musicSource.volume = 0f;
            mainMenuAudio.SFXSource.volume = 0f; 
        }
        else
        {
            /*audioManager.musicSource.volume = 0.25f;
            audioManager.SFXSource.volume = 1f;
            audioManager.countDownSource.volume = 1f;
            audioManager.suddenDeathMusicSource.volume = 1f;
            audioManager.stunSoundSource.volume = 1f;
            audioManager.floorShakeSource.volume = 1f;*/

            mainMenuAudio.musicSource.volume = 1f;
            mainMenuAudio.SFXSource.volume = 1f;
        }
    }
}