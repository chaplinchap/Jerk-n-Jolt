using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunbarScript : MonoBehaviour
{
    [SerializeField] private Image StunImage;

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;


    public void UpdateStunBar(float chargeTime, float timeTillStun ) {

        StunImage.fillAmount = chargeTime / timeTillStun;
        //StunImage.color.a = Mathf.Lerp(0, 1, chargeTime / timeTillStun);
    }

}
