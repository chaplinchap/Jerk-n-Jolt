using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] playerSprites;
    private Color startColor; 
    private Color ghostColor;
    [SerializeField] private float ghostAlpha = 0.4f;
    public bool isGhostPusher = false;
    public bool isGhostPuller = false;
    public float minGhostDuration = 1.5f;

    [SerializeField] private float ghostDuration = 3.0f;


    public bool canExitGhost = false;
    private bool ghostIsTriggeredOnce = false;


    private IEnumerator turnGhostRoutine;
    private IEnumerator minGhostRoutine;
    private BoxCollider2D playerCollider;
    private BoxCollider2D abilityCollider;

   // private BoxCollider2D otherPlayerCollider;
    public GameObject abilityGameObject;

    private int defaultLayer = 0;
    private int ghostLayer = 12;


    private void OnEnable()
    {
        ghostIsTriggeredOnce = false;
    }


    public void Start() 
    {

        playerCollider = GetComponent<BoxCollider2D>();
        abilityCollider = abilityGameObject.GetComponent<BoxCollider2D>();
       // otherPlayerCollider = otherPlayer.GetComponent<BoxCollider2D>();

        startColor = playerSprites[0].color;
        ghostColor = playerSprites[0].color;
        ghostColor.a = ghostAlpha;
        

    }

    private void Update()
    {




        if (isGhostPusher && !ghostIsTriggeredOnce || isGhostPuller && !ghostIsTriggeredOnce)
        {
            turnGhostRoutine = TurnGhost(ghostDuration);
            minGhostRoutine = MinGhostDuration(minGhostDuration);
            ghostIsTriggeredOnce = true;
            StartCoroutine(minGhostRoutine);
            StartCoroutine(turnGhostRoutine);
         
        }
        /*
        else
            StopCoroutine(turnGhostRoutine);
        */

        if(AbilityPower.hasPressedAbilityInGhostPusher && isGhostPusher && canExitGhost)
        {
            
            isGhostPusher = false;
            StopCoroutine(turnGhostRoutine);
            TurnGhostState(startColor, defaultLayer);
            ghostIsTriggeredOnce = false;
            canExitGhost = false;
            
           // otherPlayerCollider.gameObject.layer = defaultLayer;
        }

        if (AbilityPower.hasPressedAbilityInGhostPuller && isGhostPuller && canExitGhost)
        {
           
            isGhostPuller = false;
            StopCoroutine(turnGhostRoutine);
            TurnGhostState(startColor, defaultLayer);
            ghostIsTriggeredOnce = false;

            canExitGhost = false;
            
          //  otherPlayerCollider.gameObject.layer = defaultLayer;
        }


    }

    public IEnumerator TurnGhost(float duration) 
    {
      
        TurnGhostState(ghostColor, ghostLayer);
        yield return new WaitForSeconds(duration);
        TurnGhostState(startColor, defaultLayer);
   
        isGhostPusher = false;
        isGhostPuller = false;
        ghostIsTriggeredOnce = false;
        canExitGhost = false;
    }

  

    private IEnumerator MinGhostDuration (float minDuration)
    {
        yield return new WaitForSeconds(minDuration);
        canExitGhost = true;
    }
 
    private void TurnGhostState(Color color, int layer) 
    {
        
        abilityCollider.gameObject.layer = layer;
        playerCollider.gameObject.layer = layer; 

        for (int i = 0; i < playerSprites.Length; i++) 
        {
            playerSprites[i].color = color;
        }
    
    }

}
