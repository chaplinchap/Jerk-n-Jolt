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

    private void Start()
    {
        // Initialize by setting the active page to the main menu.
        activePage = MainMenu;
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
}