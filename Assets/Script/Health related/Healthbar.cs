using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 12;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 12;
    }
}
