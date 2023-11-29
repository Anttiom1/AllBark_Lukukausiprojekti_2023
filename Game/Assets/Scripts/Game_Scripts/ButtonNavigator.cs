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
    private string enterSignal;

    private void Start()
    {
        controller = FindAnyObjectByType<CustomController>();
        buttonThreshold = controller.XQuart;
    }

    void Update() // Usage: Arrow keys and enter for navigation for main menu
    {
        inputValue = controller.XQuart;
        enterSignal = controller.StartSignal;
        if (inputValue >= buttonThreshold + 20)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.XQuart;
        }

        if (inputValue <= buttonThreshold - 20)
        {
            if (currentButtonIndex == 0)
                currentButtonIndex = buttons.Length;
            currentButtonIndex = (currentButtonIndex - 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.XQuart;
        }

        if (controller.StartSignal == "1")
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }
}