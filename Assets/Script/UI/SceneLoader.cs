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
        Debug.Log("called");
        transition.SetTrigger("FadeBlack");
        Debug.Log("called?");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        Debug.Log("done");
    }

}
