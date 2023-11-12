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

    public List<HealthV2> playersHealth = new List<HealthV2>(); //reference to Health script
    public ScoreManager scoreManager; //reference to the ScoreManager

    public GameObject player1; // Used to check for players left when game is over
    public TextMeshProUGUI text;


    private void Awake()
    {
        player1 = GameObject.Find("Player1 Push"); // Find Player 1 . Used to check when game is over to see which player is left
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false); //ensure GameOVerPanel is not loaded on start
        PauseMenu.SetActive(false); //ensure pause menu is not loaded on start
    }

    private void LateUpdate()
    {
        foreach (HealthV2 playerHealth in playersHealth)
        {
            if (playerHealth.currentHealth <= 0) {
                deadPlayer = true;
                break; // No need to continue checking if one player is dead.
            }
        }
    }

    private void Update()
     {

        if (deadPlayer) {
            gameIsOver = true;
        }
   
        
        if (gameIsOver == true) {
            GameOverSequence();
                if (Input.GetKeyDown(KeyCode.Return)){                   
                    SceneManager.LoadScene(sceneToLoad);
                    Debug.Log("Game is over!");
                }
                if (Input.GetKeyDown(KeyCode.Escape)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); //Goes back to MainMenu
                }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver) // Pauses game when pressing "Escape"
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

    private bool stopChecking = true;
    public void GameOverSequence()
    {
        {
        if (!stopChecking)
        return; // Stops the code here if boolean is false

        gameOverPanel.SetActive(true);
        if (player1.activeInHierarchy){ // If player 1 is still alive
            text.text = "Player 1 Wins";
            scoreManager.UpdateScores(1,0);
            //player1Score.text = "Pusher: " + playerScore1.ToString();
        }
        else{ // If player 1 is not alive
            text.text = "Player 2 Wins";
            scoreManager.UpdateScores(0,1);
            //player2Score.text = "Pusher: " + playerScore2.ToString();
        }

        //scoreManager.UpdateScoreText();
        stopChecking = false;
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
