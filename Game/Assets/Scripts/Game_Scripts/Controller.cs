using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

// Define an interface for the controller.
public interface IController
{
    // Method to handle axis input.
    public void Axis(float horizontalValue);
    // Method to handle charge movement.
    public void ChargeMove(float charge, bool chargeDone);
}

// Create a Controller class that interacts with the IController interface.
public class Controller
{
    float charge = 0; // Initialize a charge variable to keep track of charging.
    bool chargeDone = false;
    private IController listener;

    // Constructor for the Controller class, accepting an instance of IController.
    public Controller(IController listener)
    {
        this.listener = listener;

        // Subscribe to an Update event to read controller values continuously.
        UpdateCaller.OnUpdate += ReadControllerValue;
    }

    // Method to read controller input and update the IController instance.
    public void ReadControllerValue()
    {
        // Read the horizontal input axis.
        float horizontal = Input.GetAxis("Horizontal");

        // Call the Axis method on the IController instance, passing the horizontal value.
        listener.Axis(horizontal);

        // Check if the "Jump" button is pressed.
        if (Input.GetButton("Jump"))
        {
            charge = Mathf.Clamp(charge + 15 * Time.deltaTime, 0, 50); 
            // Call the ChargeMove method on the IController instance, passing the charge value.
            listener.ChargeMove(charge,chargeDone);
        }

        // Check if the "Jump" button is released.
        if (Input.GetButtonUp("Jump"))
        {
            chargeDone = true;
            listener.ChargeMove(charge,chargeDone);
            // Reset the charge value to 0 after using it.
            charge = 0;
            chargeDone = false;
        }
    }
}
