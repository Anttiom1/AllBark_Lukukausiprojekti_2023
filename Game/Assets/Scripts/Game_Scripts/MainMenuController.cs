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
        HighScore.Instance.Show();
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
        // Hiding the leaderboard with an input when it is visible, later to be replaced with controller input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HighScore.Instance.Hide();
        }
    }
}
