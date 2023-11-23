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

}

// Create a Controller class that interacts with the IController interface.
public class Controller
{
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

    }
}
