using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedTutorial : MonoBehaviour
{
    [SerializeField] private Animator tutorialIsOver;
    [SerializeField] private float tutorialEndTimer = 5;
    private bool tutorialFinished;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!tutorialFinished)
        {
            if (collision.CompareTag("Pusher") || collision.CompareTag("Puller"))
            {
                StartCoroutine (TutorialIsComplteed());
            }
        }
    }

    IEnumerator TutorialIsComplteed()
    {
        tutorialFinished = true;
        yield return new WaitForSeconds(tutorialEndTimer);
        tutorialIsOver.SetTrigger("Show");
        Cursor.visible = true;

    }
    public void StayTutorial() //Button
    {
        tutorialIsOver.SetTrigger("Hide");
        Cursor.visible = false;
    }
}
