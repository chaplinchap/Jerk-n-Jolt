using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Platform/Despawner")]
public class PlatformDisapear : PlatformScriptableObject
{

    public Color startingColor;
    [SerializeField] Color suddenDeathColor;
 
    

    public override void CancelDespawn(GameObject target)
    {

        Color respawnColor = target.GetComponent<SpriteRenderer>().color;
        respawnColor.a = 1f;
        target.GetComponent<SpriteRenderer>().color = respawnColor;
        
    }

    public override void Despawn(GameObject target)
    {
        Color despawnColor = target.GetComponent<SpriteRenderer>().color;
        despawnColor.a = 0.1f;
        startingColor = target.GetComponent <SpriteRenderer>().color;

        target.GetComponent<SpriteRenderer>().color = despawnColor;
      
        target.GetComponent<BoxCollider2D>().enabled = false;
       
    }

    public override void Fade(GameObject target)
    {
        Color fadeColor = target.GetComponent<SpriteRenderer>().color;
      //  fadeColor.a /= 1.15f;
      fadeColor.a = 0.55f;
        target.GetComponent<SpriteRenderer>().color = fadeColor;
    }

    public override void Blink(GameObject target)
    {
        Color blinkColor = target.GetComponent<SpriteRenderer>().color;
        blinkColor.a = 0.95f;
        target.GetComponent<SpriteRenderer>().color = blinkColor;
    }


    public override void Spawn(GameObject target)
    {

        Color respawnColor = target.GetComponent<SpriteRenderer>().color;
        respawnColor.a = 1f;
        target.GetComponent<SpriteRenderer>().color = respawnColor;
        target.GetComponent<BoxCollider2D>().enabled = true;

    }

    public override void Destroy(GameObject target)
    {
        target.SetActive(false);
    }

    public override void ChangeAlpha(GameObject target)
    {
        Color AlphaColor = target.GetComponent<SpriteRenderer>().color;
        AlphaColor.a = 0.1f;
        target.GetComponent<SpriteRenderer>().color = AlphaColor;
    }


    public override void ChangeColor(GameObject target)
    {
        
        // suddenDeathColor = new Color(0.8666667f, 0.5f, 0.5f, 1f);
        target.GetComponent<SpriteRenderer>().color = suddenDeathColor;
    }
}
