using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IController
{
    protected Rigidbody rBody;           // Reference to the player's Rigidbody component.
    protected Vector3 movementVector;    // Vector for player movement.

    protected float movementSpeed;       // Speed of player movement.
    protected float rotateSpeed;         // Speed of player rotation.

    protected bool moving = false;       // Flag to indicate whether the player is moving.

    // Start is called before the first frame update
    void Start()
    {
        Init();                         // Initialize the player.
        movementSpeed = 10;             // Set the movement speed.
        rotateSpeed = 75;               // Set the rotation speed.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Add a force to the Rigidbody based on the movement vector.
        rBody.AddForce(movementVector);
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
    public virtual void ChargeMove(float charge)
    {
        // Apply velocity to the Rigidbody to move the player forward.
        GetComponent<Rigidbody>().velocity = transform.forward * charge * Time.deltaTime;

        Debug.Log(charge);  // Log the charge value.
    }

    // Handle collisions with other objects.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Wall wall = other.GetComponent<Wall>();
            moving = false;
        }
        if (other.CompareTag("Tree"))
        {
            myTree tree = other.GetComponent<myTree>();
            tree.Collide();
            moving = false;
        }
    }
}
