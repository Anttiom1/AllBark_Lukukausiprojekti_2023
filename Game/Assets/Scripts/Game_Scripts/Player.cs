using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IController
{
    [SerializeField]
    private Slider chargeMeter;
    [SerializeField]
    private Slider gasMeter;

    protected Rigidbody rBody;           // Reference to the player's Rigidbody component.
    protected Vector3 movementVector;    // Vector for player movement
    protected float movementSpeed;       // Speed of player movement.
    protected float rotateSpeed;         // Speed of player rotation.
    protected float charge;
    private float gas;
    private bool engineOn;

    Animation PlayerAnimation;
    CustomController controller;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();                         // Initialize the player.
        movementSpeed = 10;             // Set the movement speed.
        rotateSpeed = 75;               // Set the rotation speed.
        gas = 100;
        PlayerAnimation = GetComponent<Animation>();
        controller = GetComponentInChildren<CustomController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(engineOn);
        if (chargeMeter != null)
        {
            chargeMeter.value = charge;
        }
        if (gasMeter != null)
        {
            gasMeter.value = gas;
        }
        if (controller.InputValue == 1)
        {
            if (gas > 0) 
            {
                engineOn = true;
            };
        }
    }

    // Initialize the player's components.
    protected virtual void Init()
    {
        rBody = GetComponent <Rigidbody>();  // Get the player's Rigidbody component.

        if (null == rBody)
        {
            this.gameObject.SetActive(false);  // Disable the game object if the Rigidbody is not found.
            Debug.LogError("Player Init() - RigidBody not found");
        }

        // Create a Controller instance and pass 'this' (the current Player) as an IController.
        Controller myController = new Controller(this);
    }

    // Handle horizontal axis input for player rotation.
    public virtual void Axis(float horizontalValue)
    {
        // Rotate the player around the y-axis based on the horizontal input.
        transform.Rotate(Vector3.up, (horizontalValue) * rotateSpeed * Time.deltaTime);
    }

    // Handle charging movement based on charge input.
    public virtual void ChargeMove(float charge, bool chargeDone)
    {
        // Apply velocity to the Rigidbody to move the player forward.
        if (chargeDone)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * charge;
            PlayerAnimation.Play("RunAnimation");
            //UI chargemeter
            charge = 0;
        }
        this.charge = charge;
    }

    // Handle collisions with other objects.
    private void OnTriggerEnter(Collider other)
    {
        //Gets the objectManager interface from the collided object
        IObjectManager objectManager = other.GetComponent<IObjectManager>();
        //If objectManager interface is found executes the Collide method
        if (objectManager != null)
        {
            if (engineOn == true)
            {
                objectManager.Collide();
                gas = gas - 20;
                if (gas == 0)
                {
                    engineOn = false;
                }
                PlayerAnimation.Play("IdleAnimation");
                rBody.velocity = transform.forward * 0;
            }
        }
        if (objectManager == null)
        {
            Debug.Log("Ei IObjectManageria");
        }
    }  
    

}
