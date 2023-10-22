using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    //Varibles declared
    [SerializeField] private float startingHealth;
    public float currentHealth; //{get; private set;}
    public string sceneToLoad;
    public bool gameIsOver = false;

    public GameObject uim;
    private UIManager gameOver;


    private void Awake()
    {
        currentHealth = startingHealth; //Set current health to starting health (max) 
    }

    private void Start()
    {
        gameOver = uim.GetComponent<UIManager>();
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



    private void Update()
    {
        if (gameIsOver == true)
        {
            gameOver.GameOverSequence();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(sceneToLoad);
            }

        }

        //checks when current health for at least 1 of the player hit 0 then reset game
        if (currentHealth == 0)
        {

            // SceneManager.LoadScene(sceneToLoad);
            gameIsOver = true;
        }

    }
}
