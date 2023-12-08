using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting.ReorderableList;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject PauseMenu;
    public SceneLoader SceneLoader;

    public string sceneToLoad;
    public bool gameIsOver = false;
    public bool deadPlayer = false;
    private bool showGameOver; //works in correspond with gameIsOVer but have a small delay 
    private bool onPause;

    public static bool staticGameOver = false;

    public List<HealthV2> playersHealth = new List<HealthV2>(); //reference to Health script
    public ScoreManager scoreManager; //reference to the ScoreManager

    private GameObject player1; // Used to check for players left when game is over
    private GameObject player2; // Used to check for players left when game is over
    private HealthV2 player1Health;
    private HealthV2 player2Health;

    public TextMeshProUGUI text;
    public GameObject cameraAnchor;
    public GameObject cameraAnchor_ShowDraw;



    //Animations!!
    [Header("Animations")]


    [Header("PauseMenu Animations")]
    public Animator pauseOptions_Animation;
    public Animator pauseMenu_Animation;

    [Header("Game Is Over Animations")]
    public Animator Options_Animation;
    public Animator GameIsOver_Animation;
    public Animator GameIsOverButton1_Animation;
    public Animator GameIsOverButton2_Animation;
    public Animator float1_Animation;
    public Animator float2_Animation;
    public Animator winnerText_Animation;
    public Animator options_Animation; 
    public Animator showHide_Options;
    public Animator Chinemachine_zoomInDraw;

    public CinemachineVirtualCamera virtualCamera;
    
    private float targetMinOrthoSize = 8f;  // Set your target minimum orthographic size here
    private float targetMaxOrthoSize = 8f;  // Set your target maximum orthographic size here
    private float zoomDuration = 150f;         // Set the duration of the zoom

    private float initialMinOrthoSize;
    private float initialMaxOrthoSize;
    


    private void Awake()
    {
        player1 = GameObject.Find("Player1 Push"); // Find Player 1 . Used to check when game is over to see which player is left
        player2 = GameObject.Find("Player2 Pull"); // Find Player 1 . Used to check when game is over to see which player is left
        staticGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Health = player1.GetComponent<HealthV2>();
        player2Health = player2.GetComponent<HealthV2>();
        gameOverPanel.SetActive(false); //ensure GameOVerPanel is not loaded on start
        PauseMenu.SetActive(false); //ensure pause menu is not loaded on start
        Invoke ("ShowHealthbar",1);
    }

   

    private void LateUpdate()
    {
        foreach (HealthV2 playerHealth in playersHealth)
        {
            if (playerHealth.currentHealth <= 0) {
                deadPlayer = true;
            }
        }

    }

    public void Update()
     {

        if (deadPlayer) {
            gameIsOver = true;
            
        }
   
        // Send a message that the game is over
        if (gameIsOver == true) {
            StartCoroutine(GameOverSequence());     
        }

        // Show GameOver UI
        if (showGameOver == true) 
            {
            if (zoom == true)
            {
                ZoomToWinner();
            }
            staticGameOver = true;      
            Animations();
            Destroy(cameraAnchor);  
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
    private bool zoom;

    
    IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1);
        if (!showGameOver)
        {
            showGameOver = true;
            if (player1Health.currentHealth == 0 && player2Health.currentHealth == 0) // If both players have 0 health 
            {
                cameraAnchor_ShowDraw.SetActive(true);
                Chinemachine_zoomInDraw.SetTrigger("Draw");
                text.text = "Draw";
            }

            else if (player1.activeInHierarchy) // If player 1 is still alive
            {
                text.text = "Player 1 Wins";
                zoom = true;
                scoreManager.UpdateScores(1,0);
            }
               
            else if (player2.activeInHierarchy) // If player 2 is still alive
            {
                text.text = "Player 2 Wins";
                zoom = true;
                scoreManager.UpdateScores(0,1);
            }
            gameOverPanel.SetActive(true);
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
        SceneLoader.GoMenu();
    }

     public void ExitTutorial() // When clicking button
    {   
        SceneLoader.GoMenuFromTurorial();
    }

    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false; //closes the unity runner
        Application.Quit(); //closes the game
    }

    

    //============Animations================
    void Animations()
    {
        Chinemachine_zoomInDraw.SetTrigger("Winner");
        float1_Animation.SetTrigger("FloatOut");
        float2_Animation.SetTrigger("FloatOut");
        winnerText_Animation.SetTrigger("WinnerText_FloatIn");
        options_Animation.SetTrigger("WinnerText_FloatIn");
        StartCoroutine (AnimationsDelay());
    }

    void ZoomToWinner()
    {
        
        // Camera
        //CinemachineFramingTransposer framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        //initialMinOrthoSize = framingTransposer.m_MinimumOrthoSize;
        //initialMaxOrthoSize = framingTransposer.m_MaximumOrthoSize;

         //StartCoroutine(SmoothZoomCoroutine(framingTransposer));
    }

     void ShowHealthbar()
    {
        float1_Animation.SetTrigger("FloatIn");
        float2_Animation.SetTrigger("FloatIn");
    }

    private bool clickedOnce = true;
    public void ShowHideOptions() // Click button to animate
    {
            //options.SetActive(!options.activeSelf);
            if (clickedOnce)
            {
                clickedOnce = false;
                showHide_Options.SetTrigger("Show");                
            }
            else if (!clickedOnce)
            {
                clickedOnce = true;
                showHide_Options.SetTrigger("Hide");  
            }
    }

     //public Animator Options_Animation;
    //public Animator GameIsOver_Animation;

    public void GameOverOptionsButton() //animate when clicking on options on the pause menu
    {
        StartCoroutine (GoToOptions_GameOver());
    }

    public void OptionsGoBackToGameOverButton() //animate when clicking back button on the options menu on pause menu
    {
        StartCoroutine (GoToGameOver());
    }

    public void OptionsOnPauseButton() //animate when clicking on options on the pause menu
    {
        StartCoroutine (GoToOptions());
    }

    public void OptionsGoBackButton() //animate when clicking back button on the options menu on pause menu
    {
        
        StartCoroutine (GoToPause());
    }
    IEnumerator GoToOptions()
    {
        pauseMenu_Animation.SetTrigger("Hide");
        Debug.Log("animation!");
        yield return new WaitForSecondsRealtime (0.5f);
        pauseOptions_Animation.SetTrigger("Show");
    }

    IEnumerator GoToPause()
    {
        pauseOptions_Animation.SetTrigger("Hide");
        yield return new WaitForSecondsRealtime (0.5f);
        pauseMenu_Animation.SetTrigger("Show");
    }

    IEnumerator GoToOptions_GameOver()
    {
        GameIsOver_Animation.SetTrigger("Hide");
        Debug.Log("animation!");
        yield return new WaitForSecondsRealtime (0.5f);
        Options_Animation.SetTrigger("Show");
    }

    IEnumerator GoToGameOver()
    {
        Options_Animation.SetTrigger("Hide");
        yield return new WaitForSecondsRealtime (0.5f);
        GameIsOver_Animation.SetTrigger("Show");
    }

    

    IEnumerator AnimationsDelay()
    {
        yield return new WaitForSeconds(1);
        GameIsOverButton1_Animation.SetTrigger("ButtonFloatIn");       
        yield return new WaitForSeconds(1);
        GameIsOverButton2_Animation.SetTrigger("ButtonFloatIn");
    }

  /*  

     IEnumerator SmoothZoomCoroutine(CinemachineFramingTransposer framingTransposer)
    {
        float startTime = Time.time;
        float endTime = startTime + zoomDuration;



        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / zoomDuration;

            
            float newMinOrthoSize = Mathf.Lerp(initialMinOrthoSize, targetMinOrthoSize, progress);
            float newMaxOrthoSize = Mathf.Lerp(initialMaxOrthoSize, targetMaxOrthoSize, progress);

            
            framingTransposer.m_MinimumOrthoSize = newMinOrthoSize;
            framingTransposer.m_MaximumOrthoSize = newMaxOrthoSize;

            

            yield return null;
        }
    }*/
}
