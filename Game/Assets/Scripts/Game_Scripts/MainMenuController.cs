using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialWindow;
    private CustomController controller;
    private bool cooldown;

    void Start()
    {
        controller = FindAnyObjectByType<CustomController>();
    }
    public void PlayGame()
    {
        // Open game scene
        SceneManager.LoadScene(1);
        // Current bug: if the game scene is loaded a second time in a single session, the player character cannot be moved and console fills with errors
    }

    public void ShowTutorial()
    {
        tutorialWindow.SetActive(true);
        StartCoroutine(HideTutorial());
    }

    public void OpenLeaderboards()
    {
        // Leaderboards code here --
        HighScore.Instance.Show();
        StartCoroutine(WaitAndHide());
    }

    public void QuitGame()
    {
        // Quit application
        Application.Quit();
    }


    void Update()
    {
        if (controller == null)
        {
            controller = FindAnyObjectByType<CustomController>();
        }
    }
    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(5);
        HighScore.Instance.Hide();
        tutorialWindow.SetActive(false);
    }
    IEnumerator HideTutorial()
    {
        yield return new WaitForSeconds(10);
        tutorialWindow.SetActive(false);
    }
}
