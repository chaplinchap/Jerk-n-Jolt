using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    int ImageLength = 12;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / ImageLength;
    }
}
