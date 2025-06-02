using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;


    public void PlayGame()
    {
        StartCoroutine (LoadLevel());
    }

    public void Playtutorial()
    {
        StartCoroutine (LoadTutorial());
    }

    public void GoMenu()
    {
        StartCoroutine (LoadMenu());
    }

    public void GoMenuFromTurorial()
    {
        StartCoroutine (LoadMenuFromTutorial());
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
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    IEnumerator LoadTutorial ()
    {
        transition.SetTrigger("FadeBlack");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
    }

    IEnumerator LoadMenuFromTutorial ()
    {
        transition.SetTrigger("FadeBlack");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }

}
