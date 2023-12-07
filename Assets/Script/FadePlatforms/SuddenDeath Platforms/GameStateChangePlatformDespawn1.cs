using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChangePlatformDespawn1 : MonoBehaviour
{

    [SerializeField] PlatformScriptableObject platformScriptableObject;
    private float gamestateChangeTimer;
   

    private IEnumerator startGameStateChange;

     //private bool canAttachPlayer = true;


    // Start is called before the first frame update
    void Start()
    {
       gamestateChangeTimer = DeathGameChange.SuddenDeathTimer;
       startGameStateChange = startCountDown(gameObject, gamestateChangeTimer);
       StartCoroutine(startGameStateChange);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public IEnumerator startCountDown(GameObject target, float time)
    {
        
        yield return new WaitForSeconds(time);
        Debug.Log("Destroying platform starts");
        
        for (int i = 0; i <= 5;  i++)
        {
            yield return new WaitForSeconds(1f);
            platformScriptableObject.Fade(target);
        }
        DetachPlayer(target); // Deattach the player just before platform gets destroyed
        platformScriptableObject.Destroy(target);
       
    }
    private void DetachPlayer(GameObject platform)
    {
        Transform playerTransform1 = platform.transform.Find("Player1 Push");
        Transform playerTransform2 = platform.transform.Find("Player2 Pull");

            if (playerTransform1 != null) // If player is child of object
            {
                // Player 1 found
                playerTransform1.SetParent(null); // Set player to own parent
            }

            if (playerTransform2 != null) // If player is child of object
            {
                // Player 2 found
                playerTransform2.SetParent(null); // Set player to own parent
            }
    }
}
