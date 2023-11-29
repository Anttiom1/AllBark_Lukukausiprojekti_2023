using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private CustomController controller;
    private bool cooldown;

    void Start()
    {
        controller = FindAnyObjectByType<CustomController>();
        cooldown = false;
    }
    public void PlayGame()
    {
        // Open game scene
        SceneManager.LoadScene(1);
        // Current bug: if the game scene is loaded a second time in a single session, the player character cannot be moved and console fills with errors
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


    void Update()
    {
        // Hiding the leaderboard with an input when it is visible, later to be replaced with controller input
        if (controller.StartSignal == "1")
        {
            if (!cooldown)
            {
                HighScore.Instance.Hide();
                Debug.Log("test");
                cooldown = true;
                StartCoroutine(Wait());
            }
            
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
    }
}
