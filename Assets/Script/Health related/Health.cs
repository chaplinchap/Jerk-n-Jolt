using UnityEngine;

public class Health : MonoBehaviour
{
    //This script is being used by other scripts also. Be wary of changes!


    //Varibles declared
    public float startingHealth; 
    public float currentHealth; //{get; private set;}
    

    private void Awake()
    {
        currentHealth = startingHealth; //Set current health to starting health (max) 
    }

    public void TakeDamage(float damage) //When taken damage
    {
        //Current health will take x damage (later decided damage)   
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
    public void GiveHP(float Hp)
    {
        currentHealth = Mathf.Clamp(currentHealth + Hp, 0, startingHealth);
    }
}
