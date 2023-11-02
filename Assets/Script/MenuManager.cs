using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator circleWipe;
    public float transitionTime = 1f;

    private GameObject activePage;

    private void Start()
    {
        // Initialize by setting the active page to the main menu.
        activePage = MainMenu;
    }

    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Credit;
    public GameObject Lore;

    public void TransitionToPage(GameObject newPage)
    {
        if (newPage != activePage)
        {
            StartCoroutine(Transition(newPage));
        }
    }

    IEnumerator Transition(GameObject newPage)
    {
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        circleWipe.SetTrigger("End");

        activePage.SetActive(false);
        newPage.SetActive(true);

        activePage = newPage;
    }
}