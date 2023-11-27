using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] playerSprites;
    private Color startColor; 
    private Color ghostColor;
    [SerializeField] private float ghostAlpha = 0.4f;
    [SerializeField] private bool isGhost = false;
    [SerializeField] private float ghostDuration = 3.0f;
    private bool canTurnGhost;


    private BoxCollider2D playerCollider; 

    private int defaultLayer = 0;
    private int ghostLayer = 12;


    void Start() {

        playerCollider = GetComponent<BoxCollider2D>();
        
        startColor = playerSprites[0].color;
        ghostColor = playerSprites[0].color;
        ghostColor.a = ghostAlpha;
    
    }

    private void Update()
    {
        if (isGhost) 
        {
            StartCoroutine(TurnGhost(ghostDuration));
            
        }
    }

    public IEnumerator TurnGhost(float duration) 
    {
        canTurnGhost = false;
        TurnGhostState(ghostColor, ghostLayer);
        yield return new WaitForSeconds(duration);
        TurnGhostState(startColor, defaultLayer);
        isGhost = false;
        canTurnGhost = true;
        
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
