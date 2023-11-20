using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNavigator : MonoBehaviour
{
    public Button[] buttons;
    private int currentButtonIndex = 0;

    void Update() // Usage: Arrow keys and enter for navigation for main menu
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentButtonIndex == 0)
                currentButtonIndex = buttons.Length;
            currentButtonIndex = (currentButtonIndex - 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }
}