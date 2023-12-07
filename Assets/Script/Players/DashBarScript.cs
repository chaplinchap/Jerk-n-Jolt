using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarScript : MonoBehaviour
{
    private Image DashImage;
    [SerializeField] private Gradient gradient;


    private float fill = 0f;
    private float time = 0f;

    private float alphaDecreaser = 1f;

    private void Start()
    {
        UpdateGradiantAmount();
    }

    private void Awake()
    {
        DashImage = GetComponent<Image>();
    }

    public void UpdateDashBar(float chargeTime, float timeTillDash)
    {


        fill = chargeTime / timeTillDash;

        DashImage.fillAmount = fill;

        UpdateGradiantAmount();
    }

    private void UpdateGradiantAmount()
    {
        DashImage.color = gradient.Evaluate(fill);
    }

    public void Fader(float time) 
    {
        Color color = DashImage.color;

        color.a = 0.3f; 
            //Mathf.Lerp(1,0, time);

        DashImage.color = color;
    
    }
}
