using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOptions : MonoBehaviour
{

    public Image pause_Options;
    public Image gameOver_Options;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClickOptions()
    {
        StartCoroutine(EnableDisableOptions());
    }

    IEnumerator EnableDisableOptions()
    {
        float duration = 0.5f;
        float ongoingTime = 0;

        if (pause_Options != null)
        {
            while (ongoingTime < duration)
            {
                float alpha = Mathf.Lerp(0, 1, ongoingTime / duration);
                SetTextAlpha(alpha);
                ongoingTime += Time.deltaTime;
                yield return null;
            }
        }

        if (pause_Options != null)
        {
            while (ongoingTime < duration)
            {
                float alpha = Mathf.Lerp(0, 1, ongoingTime / duration);
                SetTextAlpha(alpha);
                ongoingTime += Time.deltaTime;
                yield return null;
            }
        }
        

        
    }

    void SetTextAlpha(float alpha)
    {
        Color textColor = pause_Options.color;
        textColor.a = alpha;
        pause_Options.color = textColor;
    }
}
