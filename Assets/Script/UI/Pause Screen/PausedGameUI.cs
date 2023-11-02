using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public string sceneToLoad;
    private bool onPause;

    public void ContinuebuttonPressed() { // When clickin button
        PauseMenu.SetActive(false); // Close Pause Menu
        onPause = false; // Set to false
        Time.timeScale = 1; // Make time run again
    }

    public void StartAgainButton() { // When clicking button
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Goback() // When clicking button
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); //Goes back to MainMenu
    }


    // Start is called before the first frame update
    void Start()
    {
       PauseMenu.SetActive(false); //ensure pause menu is not loaded on start
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Pauses game when pressing "Escape"
        {
            if (!onPause) // If not on Pause Menu show Pause Menu
            {
                PauseMenu.SetActive(true); // Enable Pause Menu
                Invoke (nameof (Pause), 0.00f); //This works for some reason compared to the line just under. Pause set to true
                //onPause = true; //This line does not work
                Time.timeScale = 0; // Stops time
            }  
            
            if (onPause) // If on Pause Menu disable Pause Menu
            {
                PauseMenu.SetActive(false); // disable Pause Menu
                onPause = false; // Pause set to no false
                Time.timeScale = 1; // Starts Time           
            }
        }      
    }
        void Pause()
        {
            onPause = true;
        }
} 
