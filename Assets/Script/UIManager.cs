using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);   
        
    }

    // Update is called once per frame
    void Update()
    {
     


    }

    public void GameOverSequence()
    {
        gameOverPanel.SetActive(true);
       

    }

}
