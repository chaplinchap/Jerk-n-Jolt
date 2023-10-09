using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth; //{get; private set;}
    public string sceneToLoad;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
    private void Update()
    {
        if (currentHealth == 0) 
            {
            SceneManager.LoadScene(sceneToLoad);
            }
    }
}
