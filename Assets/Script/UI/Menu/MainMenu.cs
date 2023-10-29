using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); //Goes to the gameplay
    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false; //closes the unity runner
        Application.Quit(); //closes the game
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
