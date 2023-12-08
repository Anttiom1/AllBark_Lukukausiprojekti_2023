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
        buttonThreshold = controller.ZQuart;
    }

    void Update() // Usage: Arrow keys and enter for navigation for main menu
    {
        if (controller == null)
        {
            controller = FindAnyObjectByType<CustomController>();
        }
        inputValue = controller.ZQuart;
        enterSignal = controller.StartSignal;
        /*if (inputValue >= buttonThreshold + 20)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.ZQuart;
        }*/

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.ZQuart;
        }

        /*if (inputValue <= buttonThreshold - 20)
        {
            if (currentButtonIndex == 0)
                currentButtonIndex = buttons.Length;
            currentButtonIndex = (currentButtonIndex - 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.ZQuart;
        }*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentButtonIndex == 0)
                currentButtonIndex = buttons.Length;
            currentButtonIndex = (currentButtonIndex - 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
            buttonThreshold = controller.ZQuart;
        }

        /*if (controller.Counter == 333)
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }*/
    }
}