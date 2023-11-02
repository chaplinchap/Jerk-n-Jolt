using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadePlatformPlayers : MonoBehaviour
{

    public GameObject fade1;
    public GameObject fade2;
    public GameObject fade3;
    public GameObject fade4;
    public GameObject fade5;
    public GameObject fade6;

    private float timer1;
    private float timer1Off;

    private float timer2;
    private float timer2Off;

    private float timer3;
    private float timer3Off;

    private float timer4;
    private float timer4Off;

    private float timer5;
    private float timer5Off;

    private float timer6;
    private float timer6Off;

    private float disableTime = 5f;
    private float respawnTime = 5f;
    private float recolorTime = 1f;
    private float firstFadeTime = 2f;
    private float secondFadeTime = 4f;


    private bool isOnFade1 = false;
    private bool isOnFade2 = false;
    private bool isOnFade3 = false;
    private bool isOnFade4 = false;
    private bool isOnFade5 = false;
    private bool isOnFade6 = false;

    private bool fade1Disabled = false;
    private bool fade2Disabled = false;
    private bool fade3Disabled = false;
    private bool fade4Disabled = false;        
    private bool fade5Disabled = false;
    private bool fade6Disabled = false;




    // Start is called before the first frame update
    void Start()
    {
        fade1 = GameObject.FindWithTag("FadingPlatform");
        fade2 = GameObject.FindWithTag("FadingPlatform1");
        fade3 = GameObject.FindWithTag("FadingPlatform2");
        fade4 = GameObject.FindWithTag("FadingPlatform3");
        fade5 = GameObject.FindWithTag("FadingPlatform4");
        fade6 = GameObject.FindWithTag("FadingPlatform5");



    }

    // Update is called once per frame
    void Update()
    {

        //1st platform:
        if (isOnFade1 && Time.time - timer1 > firstFadeTime)
        {
            fade1.GetComponent<SpriteRenderer>().color = new Color (150, 0, 0, 0.5f);

            if (isOnFade1 && Time.time - timer1 > secondFadeTime)
            {
                fade1.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade1 && Time.time - timer1 > disableTime)
                {
                    fade1.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);                   
                    fade1.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade1 = false;
                    fade1Disabled = true;
                }
            }
        }

        if (isOnFade1 == false && !fade1Disabled && Time.time - timer1Off > recolorTime)
        {
            fade1.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
                       
        }

        if(fade1Disabled == true && Time.time - timer1Off > respawnTime)
        {
            fade1.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade1.GetComponent<BoxCollider2D>().enabled = true;
            fade1Disabled = false;

        }

        //2nd platform:
        if (isOnFade2 && Time.time - timer2 > firstFadeTime)
        {
            fade2.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.5f);

            if (isOnFade2 && Time.time - timer2 > secondFadeTime)
            {
                fade2.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade2 && Time.time - timer2 > disableTime)
                {
                    fade2.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);
                    fade2.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade2 = false;
                }
            }
        }

        if (isOnFade2 == false && Time.time - timer2Off > recolorTime)
        {
            fade2.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade2.GetComponent<BoxCollider2D>().enabled = true;
        }

        //3rd platform:
        if (isOnFade3 && Time.time - timer3> firstFadeTime)
        {
            fade3.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.5f);

            if (isOnFade3 && Time.time - timer3 > secondFadeTime)
            {
                fade3.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade3 && Time.time - timer3 > disableTime)
                {
                    fade3.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);
                    fade3.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade3 = false;
                }
            }
        }

        if (isOnFade3 == false && Time.time - timer3Off > recolorTime)
        {
            fade3.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade3.GetComponent<BoxCollider2D>().enabled = true;
        }


        //4th platform:
        if (isOnFade4 && Time.time - timer4 > firstFadeTime)
        {
            fade4.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.5f);

            if (isOnFade4 && Time.time - timer4 > secondFadeTime)
            {
                fade4.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade4 && Time.time - timer4 > disableTime)
                {
                    fade4.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);
                    fade4.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade4 = false;
                }
            }
        }

        if (isOnFade4 == false && Time.time - timer4Off > recolorTime)
        {
            fade4.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade4.GetComponent<BoxCollider2D>().enabled = true;
        }


        //5th platform:
        if (isOnFade5 && Time.time - timer5 > firstFadeTime)
        {
            fade5.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.5f);

            if (isOnFade5 && Time.time - timer5 > secondFadeTime)
            {
                fade5.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade5 && Time.time - timer5 > disableTime)
                {
                    fade5.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);
                    fade5.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade5 = false;
                }
            }
        }

        if (isOnFade5 == false && Time.time - timer5Off > recolorTime)
        {
            fade5.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade5.GetComponent<BoxCollider2D>().enabled = true;
        }


        //6th platform:
        if (isOnFade6 && Time.time - timer6 > firstFadeTime)
        {
            fade6.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.5f);

            if (isOnFade6 && Time.time - timer6 > secondFadeTime)
            {
                fade6.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.25f);

                if (isOnFade6 && Time.time - timer6 > disableTime)
                {
                    fade6.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 0.1f);
                    fade6.GetComponent<BoxCollider2D>().enabled = false;
                    isOnFade6 = false;
                }
            }
        }

        if (isOnFade6 == false && Time.time - timer6Off > recolorTime)
        {
            fade6.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 1f);
            fade6.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FadingPlatform"))
        {
            timer1 = Time.time;
            isOnFade1 = true;           
                       
        }

        if (collision.CompareTag("FadingPlatform1"))
        {
            timer2 = Time.time;
            isOnFade2 = true;

        }

        if (collision.CompareTag("FadingPlatform2"))
        {
            timer3 = Time.time;
            isOnFade3 = true;
        }

        if (collision.CompareTag("FadingPlatform3"))
        {
            timer4 = Time.time;
            isOnFade4 = true;
        }

        if (collision.CompareTag("FadingPlatform4"))
        {
            timer5 = Time.time;
            isOnFade5 = true;
        }

        if (collision.CompareTag("FadingPlatform5"))
        {
            timer6 = Time.time;
            isOnFade6 = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FadingPlatform"))
        {
            isOnFade1 = false;
            timer1Off = Time.time;           
        }

        if (collision.CompareTag("FadingPlatform1"))
        {
            isOnFade2 = false;
            timer2Off = Time.time;
        }

        if (collision.CompareTag("FadingPlatform2"))
        {
            isOnFade3 = false;
            timer3Off = Time.time;
        }

        if (collision.CompareTag("FadingPlatform3"))
        {
            isOnFade4 = false;
            timer4Off = Time.time;
        }

        if (collision.CompareTag("FadingPlatform4"))
        {
            isOnFade5 = false;
            timer5Off = Time.time;
        }

        if (collision.CompareTag("FadingPlatform5"))
        {
            isOnFade6 = false;
            timer6Off = Time.time;
        }


    }

}
