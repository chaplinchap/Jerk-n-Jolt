using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCircle : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    private Sprite sprite; 

    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private int timesColorChange = 3;
    [SerializeField] private float changeRateColor = .15f;

    [SerializeField] private int changeRateSize = 90;
    [SerializeField] private int circleSize = 10;

    [SerializeField] private Color swapColor1;
    [SerializeField] private Color swapColor2;





    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        DeathCircleAnimation();
        
    }


    private void ChangeSprite() 
    {

        int randomSprite = Random.Range(0, sprites.Count);

        spriteRend.sprite = sprites[randomSprite];

    }

    private IEnumerator TurnColor()
    {
        float alpha = spriteRend.color.a;


        for (int i = 0; i < timesColorChange; i++)
        {
            spriteRend.color = swapColor1;
            yield return new WaitForSeconds(changeRateColor);
            spriteRend.color = swapColor2;
            yield return new WaitForSeconds(changeRateColor);
        }

        Destroy(gameObject);
    }

    private IEnumerator ChangeSize()
    {
        for (int i = 1; i < changeRateSize; i++)
        {
            transform.localScale += circleSize*Vector3.one/(i);
            yield return new WaitForFixedUpdate();

        }
    }


    private void DeathCircleAnimation() {

        ChangeSprite();
        StartCoroutine(TurnColor());
        StartCoroutine(ChangeSize());
    }

}
