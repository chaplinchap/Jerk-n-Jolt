using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerSuddenDeath : MonoBehaviour
{
    public List<Slider> timerSlider;
    public List<Image> sliderFill;

    //public Image sliderfill1;

    public float sliderTimer;
    public bool stopTimer;
    public UIManager manager;
    public bool endAnimation = true;
    public TextMeshProUGUI Text_SuddenDeath;
    [SerializeField] private Gradient gradient;

    [Header("Animations")]
    public Animator SuddenDeath_animation;

    private float startTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        sliderTimer = DeathGameChange.SuddenDeathTimer;
        sliderTimer -= startTimer + 0.3f;

        foreach (Slider slider in timerSlider)
        {
            slider.maxValue = sliderTimer;
            slider.value = sliderTimer;
        }
        
        //StartTimer();
        Invoke ("StartAnimation",2);
        Invoke ("StartTimer", startTimer);

        //Text_SuddenDeath.enabled = false;
        SetTextAlpha(0f);

    }

    void StartAnimation()
    {
        SuddenDeath_animation.SetTrigger("Start");
    }

    void EndAnimation()
    {
        SuddenDeath_animation.SetTrigger("End");   
    }

    // Update is called once per frame
    public void Update()
    {
        if (manager.gameIsOver && endAnimation) // If game is over
        {
            Invoke ("EndAnimation",1); // Small delay before starting animation  
            endAnimation = false; // So it wont repeat
            Text_SuddenDeath.enabled = false;
        }

        if (stopTimer && endAnimation) // If timer has been stopped 
        {
            Invoke ("EndAnimation",1); // Small delay before starting animation  
            StartCoroutine (SuddenDeathMessage());
            endAnimation = false; // So it wont repeat
        }
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker()
    {

        while (stopTimer == false)
        {
            sliderTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (sliderTimer <= 0)
            {
                stopTimer = true; // When slider reaches 0, stopTimer will be set to true
            }

            if (stopTimer == false)
            {
                foreach(Slider slider in timerSlider)
                {
                    slider.value = sliderTimer;
                    //sliderfill1.color = gradient.Evaluate(slider.value/100);
                    
                    foreach(Image sliderColor in sliderFill)
                    {
                    //slider.value = sliderTimer; 
                    //sliderColor.color = Color.Lerp(Color.green, Color.red, slider.value/90);
                    sliderColor.color = gradient.Evaluate(slider.value/DeathGameChange.SuddenDeathTimer);
                        
                    }
                }

                
                
            }
        }        
    }

    IEnumerator SuddenDeathMessage()
    {
        float duration = 0.5f;
        float ongoingTime = 0;

        while (ongoingTime < duration)
        {
            float alpha = Mathf.Lerp(0,1, ongoingTime / duration);
            SetTextAlpha(alpha);
            ongoingTime += Time.deltaTime;
            yield return null;
        }

        SetTextAlpha(1);

        yield return new WaitForSeconds (2);

        ongoingTime = 0;

        while (ongoingTime < duration)
        {
            float alpha = Mathf.Lerp(1,0, ongoingTime / duration);
            SetTextAlpha(alpha);
            ongoingTime += Time.deltaTime;
            yield return null;
        }

        SetTextAlpha(0);       
    }

    void SetTextAlpha(float alpha)
    {
        Color textColor = Text_SuddenDeath.color;
        textColor.a = alpha;
        Text_SuddenDeath.color = textColor;
    }
}
