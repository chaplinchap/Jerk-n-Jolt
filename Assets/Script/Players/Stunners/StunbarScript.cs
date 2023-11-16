using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunbarScript : MonoBehaviour
{
    private Image StunImage;
    [SerializeField] private Gradient gradient;

    private float fill = 0f; 

    private void Start() 
    {
        StunImage = GetComponent<Image>();

        UpdateGradiantAmount(); 
    }

    public void UpdateStunBar(float chargeTime, float timeTillStun) {
            
        fill = chargeTime / timeTillStun;

        StunImage.fillAmount = fill;

        UpdateGradiantAmount();
    }

    private void UpdateGradiantAmount() 
    {
        StunImage.color = gradient.Evaluate(fill);
    }
}
