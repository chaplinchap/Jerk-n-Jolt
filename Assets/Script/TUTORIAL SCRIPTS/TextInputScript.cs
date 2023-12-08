using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInputScript : MonoBehaviour
{

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        SetText("Work") ;
        
    }

    
    public void SetText(string newText) 
    {
        text.text = newText;
    }
     
}
