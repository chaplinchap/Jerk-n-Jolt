using System.Collections;
using TMPro;
using UnityEngine;

public class GameStartCountDown : MonoBehaviour
{
    public float timeRemaining = 3; //How long to wait for game to start
    public GameObject puller; 
    public GameObject pusher;
    public TextMeshProUGUI countDownText;  



    private void Start()
    {
        //On start set player movement to false so they cant move
        puller.GetComponent<PlayerMovement>().enabled = false; 
        pusher.GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine (CountDownText()); //Start a coundown
    }

    IEnumerator CountDownText()
    {
        //While time remaining is higher than 0, update countdown
        while (timeRemaining > 0)
        {
            countDownText.text = timeRemaining.ToString(); //Updates the text
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }

        //When time remaining is 0 start game. 
        countDownText.text = "Fight!";
        puller.GetComponent<PlayerMovement>().enabled = true;
        pusher.GetComponent<PlayerMovement>().enabled = true;
        Invoke (nameof (removeText), 1);//Fight text will stay for 1 second before being set to false

    }

    void removeText()
    {
        countDownText.gameObject.SetActive(false); 
    }
}
