using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarScript : MonoBehaviour
{
    private Image DashImage;
    [SerializeField] private Color color;
    private MovementAid movementAid;

    private float fill = 0f;

    [SerializeField] private float duration = 0.2f;
    private bool turnDashBarOn = true;

    

    private void Start()
    {
        movementAid = GetComponentInParent<MovementAid>();
        
        DashImage = GetComponent<Image>();
        DashImage.color = color;
        DashImage.fillAmount = 0;
        turnDashBarOn = false; 
    }

    public void UpdateDashBar(bool turn)
    {
        if (turn && turnDashBarOn)
        {
            StartCoroutine(DashBarFlash(duration));
        }
    }




    private IEnumerator DashBarFlash(float duration) 
    {
        Filler(1);
        yield return new WaitForSeconds(duration);
        Filler(0);
    }

    private void Filler(float fillAmount) 
    {
        fill = fillAmount;
        DashImage.fillAmount = fill;
    }

    private void Update() 
    {
        UpdateDashBar(movementAid.CanDash());
        GetDashClass();
    }


    private void GetDashClass() 
    {
        if (movementAid.CanDash())
        {
            turnDashBarOn = false;
        }
        else
        {
            turnDashBarOn = true;
        }
    }
}
