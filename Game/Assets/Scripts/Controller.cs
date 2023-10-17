using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public interface IController
{
    public void Axis(float horizontalValue, float verticalValue);
    
}

public class Controller
{
    private IController listener;

    public Controller(IController listener)
    {
        this.listener = listener;

        UpdateCaller.OnUpdate += ReadControllerValue;
    }

    public void ReadControllerValue()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        listener.Axis(horizontal, vertical);
    }
    
    
}

