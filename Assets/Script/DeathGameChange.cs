using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathGameChange : MonoBehaviour
{
    //Varibles declared
    [Header("Pusher Settings")]
    public GameObject Pusher;
    public HealthV2 Push_healthScript; //reference to Health script
    //public Healthbar Push_healthbarScript; //reference to Healthbar script
    [SerializeField] private Image Push_totalHealthbar; //UI of players current healthbar (foreground)
    
    [Header("Puller Settings")]
    public GameObject Puller;
    public HealthV2 Pul_healthScript; //reference to Health script
    //public Healthbar Pul_healthbarScript; //reference to Healthbar script
    [SerializeField] private Image Pull_totalHealthbar; //UI of players current healthbar (foreground)
    

    [Header("SuddenDeath Settings")]
    public static float SuddenDeathTimer = 10; // deside when sudden Death change to happen
    public bool suddenDeathTriggered = false;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SuddenDeath());
    }

    private IEnumerator SuddenDeath()
    {
        yield return new WaitForSeconds(SuddenDeathTimer);

        suddenDeathTriggered = true;

        // Set players current health to 1
        Pul_healthScript.currentHealth = 1; 
        Push_healthScript.currentHealth = 1;

        // Set so players only can get max 1 heart
        Pul_healthScript.maxHealth = 1;
        Push_healthScript.maxHealth = 1;

        // Visually shows that total health is 1 (background color of hearths)
        Push_totalHealthbar.fillAmount = 0.0865f; // 1/12 = 0.0833 allowing only 1 totalHearth showing. little higher since else it wouldnt show whole heart
        Pull_totalHealthbar.fillAmount = 0.0865f;
        Debug.Log("Sudden Death!");
    }

}
