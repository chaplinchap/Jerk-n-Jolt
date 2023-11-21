using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Audiosystem
    MainMenuAudio audioManagerMainMenu;
    
    public void Start()
    {
        audioManagerMainMenu = GameObject.FindGameObjectWithTag("Audio").GetComponent<MainMenuAudio>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); //Goes to the gameplay
    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false; //closes the unity runner
        Application.Quit(); //closes the game
    }
    public void ButtonClick()
    {
        audioManagerMainMenu.PlaySFX(audioManagerMainMenu.buttonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    
}
