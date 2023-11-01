using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(menuName = "Platform/Despawner")]
public class PlatformDisapear : PlatformScriptableObject
{

    public Color startingColor;

    public override void Despawn(GameObject target) {

        Color color = target.GetComponent<SpriteRenderer>().color;
        color.a = 0.1f;

        startingColor = target.GetComponent<SpriteRenderer>().color;

        target.GetComponent<SpriteRenderer>().color = color;
        target.GetComponent<BoxCollider2D>().enabled = false;
    
    }

    public override void Spawn(GameObject target) {

        target.GetComponent<SpriteRenderer>().color = startingColor;
        target.GetComponent<BoxCollider2D>().enabled = true;

    }

    public override void Fade(GameObject target)
    {
        Color color = target.GetComponent<SpriteRenderer>().color;
        color.a /= 1.2f;
        target.GetComponent <SpriteRenderer>().color = color;
    }

    public override void DeFade(GameObject target)
    {
        target.GetComponent <SpriteRenderer>().color = Color.magenta;
    }


}
