using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    int ImageLength = 12; //Image length has 12 hearts
    [SerializeField] private HealthV2 playerHealth; //Input player health
    [SerializeField] private Image totalHealthbar; //UI of healthbar (background)
    [SerializeField] private Image currentHealthbar; //UI of players current healthbar (foreground)

    private void Start()
    {
        //Access to image fill amount. players starting health (3) devided by ImageLenght (12) 
        totalHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
        //totalHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
    }

    private void Update()
    {
        //Access to image fill amount. look for what player health is. when losing health change fill amount
        currentHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
    }
}
