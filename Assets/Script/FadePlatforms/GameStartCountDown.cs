using System.Collections;
using TMPro;
using UnityEngine;

public class GameStartCountDown : MonoBehaviour
{
    public float timeRemaining = 3; //How long to wait for game to start

    [Header("Puller")]
    public GameObject puller;

    [Header("Pusher")]
    public GameObject pusher;


    [Header("Other Stuff")]
    public TextMeshProUGUI countDownText;
    public Animator countDown_Animation;



    private void Start()
    {
        //On start set player movement to false so they cant move
        puller.GetComponent<PlayerMovement>().enabled = false;
        pusher.GetComponent<PlayerMovement>().enabled = false;

        puller.GetComponent<Pull>().enabled = false;
        pusher.GetComponent<Push>().enabled = false;

        puller.GetComponent<Dash>().enabled = false;
        pusher.GetComponent<Dash>().enabled = false;

        StartCoroutine(CountDownText()); //Start a coundown
    }

    IEnumerator CountDownText()
    {
        //While time remaining is higher than 0, update countdown
        while (timeRemaining > 0)
        {
            countDownText.text = timeRemaining.ToString(); //Updates the text
            countDown_Animation.SetTrigger("Flop");
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }

        //When time remaining is 0 start game. 
        countDownText.text = "Fight!"; //Announces Fight to let players know you can move
        countDown_Animation.SetTrigger("Flop");
        puller.GetComponent<PlayerMovement>().enabled = true;
        pusher.GetComponent<PlayerMovement>().enabled = true;

        puller.GetComponent<Pull>().enabled = true;
        pusher.GetComponent<Push>().enabled = true;

        puller.GetComponent<Dash>().enabled = true;
        pusher.GetComponent<Dash>().enabled = true;

        StartCoroutine(RemoveText());
        //Invoke (nameof (removeText), 1);//Fight text will stay for 2 second before being set to false

    }

    /*void removeText()
    {
        countDown_Animation.SetTrigger("FloatOut");
        countDownText.gameObject.SetActive(false); 
    }*/
    private IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(1);
        countDown_Animation.SetTrigger("FloatOut");
        yield return new WaitForSeconds(1);
        countDownText.gameObject.SetActive(false);
    }
}