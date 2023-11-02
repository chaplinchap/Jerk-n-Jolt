using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject PauseMenu;

    public string sceneToLoad;
    public bool gameIsOver = false;
    public bool deadPlayer = false;
    private bool onPause;

    public List<Health> playersHealth = new List<Health>(); //reference to Health script

    private GameObject player1; // Varible given

    private void Awake()
    {
        player1 = GameObject.Find("Player1 Push"); // Find Player 1 
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false); //ensure GameOVerPanel is not loaded on start
        PauseMenu.SetActive(false); //ensure pause menu is not loaded on start
    }

    public TextMeshProUGUI text;
     private void Update()
     {
        foreach (Health playerHealth in playersHealth)
        {
            if (playerHealth.currentHealth <= 0) {
                deadPlayer = true;
                break; // No need to continue checking if one player is dead.
            }
        }

        if (deadPlayer) {
            gameIsOver = true;
        }
   
        
        if (gameIsOver == true) {
            GameOverSequence();
                if (Input.GetKeyDown(KeyCode.Return)){
                    SceneManager.LoadScene(sceneToLoad);
                    Debug.Log("Game is over!");
                }
        }

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

    public void GameOverSequence()
    {
        gameOverPanel.SetActive(true);
        if (player1.activeInHierarchy) // If player 1 is still alive 
        {
            text.text = "Player 1 Wins";
        }
        else // If player 1 is not alive
        {
            text.text = "Player 2 Wins";
        }
    }

    //=============== <Buttons> ===============
     public void ContinueButton() { // When clicking button
        PauseMenu.SetActive(false); // Close Pause Menu
        onPause = false; // Set to false
        Time.timeScale = 1; // Make time run again
    }

    public void StartAgainButton() { // When clicking button
        SceneManager.LoadScene(sceneToLoad); // Loads the current scene
    }

    public void GoBackButton() // When clicking button
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); //Goes back to MainMenu
    }


}
