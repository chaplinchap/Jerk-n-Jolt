using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PausedGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Continue; //Name of button
    [SerializeField] private TextMeshProUGUI Exit; //Name of button
    [SerializeField] private TextMeshProUGUI StartAgain; //Name of button
    [SerializeField] private GameObject PauseMenu;
    public string sceneToLoad;
    private bool onPause = false;

    public void ContinuebuttonPressed()
    {
        PauseMenu.SetActive(false);
        onPause = false;
        Time.timeScale = 1;
        Debug.Log("You pressed continue");
    }

    public void ExitbuttonPressed()
    {
        Debug.Log("You pressed exit");
    }

    public void StartAgainButton()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


    // Start is called before the first frame update
    void Start()
    {
       PauseMenu.SetActive(false); 
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onPause)
            {
                PauseMenu.SetActive(true);
                Invoke (nameof (Pause), 0.00f); //This works for some reason compared to the line just under
                //onPause = true;
                Time.timeScale = 0; //Stops movement
                Debug.Log("You pressed pause");
            }  
            
            if (onPause)
            {
                PauseMenu.SetActive(false);
                onPause = false;
                Time.timeScale = 1;
                Debug.Log("You exited pause");               
            }
        }      
    }
        void Pause()
        {
            onPause = true;
        }
} 
