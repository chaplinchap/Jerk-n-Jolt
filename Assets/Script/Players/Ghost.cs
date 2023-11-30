using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] playerSprites;
    private Color startColor; 
    private Color ghostColor;
    [SerializeField] private float ghostAlpha = 0.4f;
    public bool isGhost = false;
    [SerializeField] private float ghostDuration = 3.0f;
    // private bool canTurnGhost;

    private bool ghostIsTriggeredOnce = false;


    private IEnumerator turnGhostRoutine;
    private BoxCollider2D playerCollider;
    private BoxCollider2D otherPlayerCollider;
    public GameObject otherPlayer;

    private int defaultLayer = 0;
    private int ghostLayer = 12;


   public void Start() 
    {

        playerCollider = GetComponent<BoxCollider2D>();
        otherPlayerCollider = otherPlayer.GetComponent<BoxCollider2D>();

        startColor = playerSprites[0].color;
        ghostColor = playerSprites[0].color;
        ghostColor.a = ghostAlpha;
        

    }

    private void Update()
    {
        if (isGhost && !ghostIsTriggeredOnce)
        {
            turnGhostRoutine = TurnGhost(ghostDuration);
            ghostIsTriggeredOnce = true;
            StartCoroutine(turnGhostRoutine);
        }
        /*
        else
            StopCoroutine(turnGhostRoutine);
        */
    }

    public IEnumerator TurnGhost(float duration) 
    {
      
        TurnGhostState(ghostColor, ghostLayer);
        otherPlayerCollider.gameObject.layer = ghostLayer;     
        yield return new WaitForSeconds(duration);
        TurnGhostState(startColor, defaultLayer);
        otherPlayerCollider.gameObject.layer = defaultLayer;
        isGhost = false;
        ghostIsTriggeredOnce = false;
   
        
    }


    private void TurnGhostState(Color color, int layer) 
    {
        

        playerCollider.gameObject.layer = layer; 

        for (int i = 0; i < playerSprites.Length; i++) 
        {
            playerSprites[i].color = color;
        }
    
    }

}
