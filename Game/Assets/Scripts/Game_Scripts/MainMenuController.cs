using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Open game scene
        SceneManager.LoadScene(1);
    }

    public void OpenLeaderboards()
    {
        // Leaderboards code here --
    }

    public void QuitGame()
    {
        // Quit application
        Application.Quit();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
