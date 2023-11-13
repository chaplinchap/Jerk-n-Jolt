using TMPro;
using UnityEngine;

// This script is refered to in the UIManager script

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;

    public int playerScore1 = 0;
    public int playerScore2 = 0;


    // Start is called before the first frame update
    void Start()
    {
        

        playerScore1 = PlayerPrefs.GetInt("PlayerScore1", 0);
        playerScore2 = PlayerPrefs.GetInt("PlayerScore2", 0);
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText()
    {
        player1Score.text = "Pusher: " + playerScore1.ToString();
        player2Score.text = "Puller: " + playerScore2.ToString();
        PlayerPrefs.Save();
    }

    // New method to update scores and save changes
    public void UpdateScores(int score1, int score2)
    {
        playerScore1 += score1;
        playerScore2 += score2;
        PlayerPrefs.SetInt("PlayerScore1", playerScore1);
        PlayerPrefs.SetInt("PlayerScore2", playerScore2);
        PlayerPrefs.Save();
        UpdateScoreText();
    }

     public void ResetScore() // When clicking button, reset score and save
    {
        PlayerPrefs.SetInt("PlayerScore1", playerScore1 = 0);
        PlayerPrefs.SetInt("PlayerScore2", playerScore2 = 0);
        PlayerPrefs.Save();
        UpdateScoreText();
    }
}
