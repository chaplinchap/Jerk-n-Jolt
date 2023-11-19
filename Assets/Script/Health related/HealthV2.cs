using UnityEngine;
using UnityEngine.UI;

public class HealthV2 : MonoBehaviour
{
    //Varibles declared
    public float maxHealth = 3;  //Maximum health the player can get
    public float currentHealth; //Current health the player has
    public Healthbar healthbar;


     private void Awake()
    {
        currentHealth = maxHealth; //Set current health to max health 
    }

    public void TakeDamage(float damage) //When taken damage
    {
        //Current health will take x damage (damage is decided in the TakeDamage-Script)   
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthbar.ShakeObject();
    }
    public void GiveHP(float Hp)
    {
        //Current health will get + health back (hp is decided from the give hp Script)
        currentHealth = Mathf.Clamp(currentHealth + Hp, 0, maxHealth);
    }
}
