using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNavigator : MonoBehaviour
{
    public Button[] buttons;
    private int currentButtonIndex = 0;
    private CustomController controller;
    private int inputValue;
    private int buttonThreshold;

    private void Start()
    {
        controller = FindAnyObjectByType<CustomController>();
        buttonThreshold = controller.YQuart;
    }

    void Update() // Usage: Arrow keys and enter for navigation for main menu
    {
        inputValue = controller.YQuart;
        if (inputValue >= buttonThreshold + 10)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.YQuart;
        }

        if (inputValue <= buttonThreshold - 10)
        {
            if (currentButtonIndex == 0)
                currentButtonIndex = buttons.Length;
            currentButtonIndex = (currentButtonIndex - 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.YQuart;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }
}