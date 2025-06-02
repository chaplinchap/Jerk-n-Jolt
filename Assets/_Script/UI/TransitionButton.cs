using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButton : MonoBehaviour
{
    public Animator circleWipe; 
    public float transitionTime = 0.5f;
  
    [Header("Pages")] 
    public GameObject options;
    public GameObject Credit;
    public GameObject Lore;
    public GameObject MainMenu;
    


    public void OptionsButton()
    {
        StartCoroutine(Options());
    }

    IEnumerator Options()
    {
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        circleWipe.SetTrigger("End");
        options.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void BackButton()
    {
        StartCoroutine(Back());
    }

    IEnumerator Back()
    { 
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        circleWipe.SetTrigger("End");
        options.SetActive(false);
        MainMenu.SetActive(true);
    }
}
