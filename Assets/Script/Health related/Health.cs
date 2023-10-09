using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Varibles declared
    [SerializeField] private float startingHealth; 
    public float currentHealth; //{get; private set;}
    public string sceneToLoad;

    private void Awake()
    {
        currentHealth = startingHealth; //Set current health to starting health (max) 
    }

    public void TakeDamage(float damage) //When taken damage
    {
        //Current health will take x damage (later decided damage)   
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
    private void Update()
    {
        //checks when current health for at least 1 of the player hit 0 then reset game
        if (currentHealth == 0) 
            {
            SceneManager.LoadScene(sceneToLoad);
            }
    }
}
