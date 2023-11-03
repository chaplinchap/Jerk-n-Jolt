using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChangePlatformDespawns : MonoBehaviour
{

    [SerializeField] PlatformScriptableObject platformScriptableObject;
    private float gamestateChangeTimer = 120f;
   

    private IEnumerator startGameStateChange;


    // Start is called before the first frame update
    void Start()
    {
       startGameStateChange = startCountDown(gameObject, gamestateChangeTimer);
       StartCoroutine(startGameStateChange);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public IEnumerator startCountDown(GameObject target, float time)
    {
        Debug.Log("CountDown starts");
        
        yield return new WaitForSeconds(time);
        Debug.Log("Destroying platform starts");
        for (int i = 0; i <= 5;  i++)
        {
            yield return new WaitForSeconds(1f);
            platformScriptableObject.Fade(target);
        }

        platformScriptableObject.Destroy(target);
    }
}
