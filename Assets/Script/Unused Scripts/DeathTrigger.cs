using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{

    public string sceneToLoad;


    private void OnTriggerEnter2D(Collider2D deathTrigger)
    {
        if (deathTrigger.gameObject.CompareTag("Pusher") || deathTrigger.gameObject.CompareTag("Puller"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
