using UnityEngine;
using UnityEngine.UI;

public class HealthV2 : MonoBehaviour
{
    //Varibles declared
    public float maxHealth = 3;  //Maximum health the player can get
    public float currentHealth; //Current health the player has 
    int ImageLength = 12; //Image length has 12 hearts = can support up to 12 hearths

    //[SerializeField] private Health playerHealth; //Input player health
    [SerializeField] private Image totalHealthbar; //UI of healthbar (background)
    [SerializeField] private Image currentHealthbar; //UI of players current healthbar (foreground)

     private void Awake()
    {
        currentHealth = maxHealth; //Set current health to max health 
    }

    private void Start()
    {
        //Access to image fill amount. players starting health (3) devided by ImageLenght (12) 
        totalHealthbar.fillAmount = currentHealth / ImageLength;
    }

    private void Update()
    {
        //Access to image fill amount. look for what player health is. when losing health change fill amount
        currentHealthbar.fillAmount = currentHealth / ImageLength;
    }

    public void TakeDamage(float damage) //When taken damage
    {
        //Current health will take x damage (damage is decided in the TakeDamage-Script)   
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
    }
    public void GiveHP(float Hp)
    {
        //Current health will get + health back (hp is decided from the give hp Script)
        currentHealth = Mathf.Clamp(currentHealth + Hp, 0, maxHealth);
    }
}
