using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) 
          //  {
            //    LoadScene();
            //}
    }

    //public void LoadSceness()
    //{
    //   StartCoroutine (LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    // }

    public void PlayGame()
    {
        StartCoroutine (LoadLevel());
    }

    public void GoMenu()
    {
        StartCoroutine (LoadMenu());
    }

    IEnumerator LoadLevel ()
    {
        transition.SetTrigger("FadeBlack");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    IEnumerator LoadMenu ()
    {
        transition.SetTrigger("FadeBlack");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

}
