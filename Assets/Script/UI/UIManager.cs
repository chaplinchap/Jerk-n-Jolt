using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    public string sceneToLoad;
    public bool gameIsOver = false;
    public bool deadPlayer = false;
    public List<Health> playersHealth = new List<Health>(); //reference to Health script

    public GameObject player1; 

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
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
     }

    public void GameOverSequence()
    {
        gameOverPanel.SetActive(true);
        if (player1.activeInHierarchy)
        {
            text.text = "Player 1 Wins";
        }
        else
        {
            text.text = "Player 2 Wins";
        }
    }

}
