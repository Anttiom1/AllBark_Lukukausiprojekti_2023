using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Slider gasMeter;

    protected Rigidbody rBody;           // Reference to the player's Rigidbody component.
    protected Vector3 movementVector;    // Vector for player movement
    protected float movementSpeed;       // Speed of player movement.
    protected float rotateSpeed;         // Speed of player rotation.
    private float gas;
    private bool engineOn;
    private int zThreshold;


    Terrain terrain;
    Animation PlayerAnimation;
    CustomController controller;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();                         // Initialize the player.
        movementSpeed = 10;             // Set the movement speed.
        rotateSpeed = 5;               // Set the rotation speed.
        gas = 100;
        PlayerAnimation = GetComponent<Animation>();
        controller = FindAnyObjectByType<CustomController>();
        engineOn = false;
        terrain = FindAnyObjectByType<Terrain>();
        zThreshold = controller.ZQuart;
    }

    // Update is called once per frame
    void Update()
    {
        gasMeter.value = gas;
        Axis();
        if(controller.Counter == 666)
        {
            engineOn = true;
        }
    }

    void FixedUpdate()
    {
        if (engineOn)
        {
            rBody.velocity = transform.forward * movementSpeed;
            float y = terrain.SampleHeight(new Vector3(rBody.transform.position.x, 0, rBody.transform.position.z));
            Vector3 newPos = new Vector3(rBody.position.x, y+1, rBody.position.z);
            rBody.MovePosition(newPos);
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
    }

    // Handle horizontal axis input for player rotation.
    public void Axis()
    {
        // Rotate the player around the y-axis based on the horizontal input.
        transform.Rotate(Vector3.up, controller.ZQuart * rotateSpeed * Time.deltaTime);
    }

    // Handle collisions with other objects.
    private void OnTriggerEnter(Collider other)
    {
        //Gets the objectManager interface from the collided object
        IObjectManager objectManager = other.GetComponent<IObjectManager>();
        //If objectManager interface is found executes the Collide method
        if (objectManager != null)
        {   
            gas = Mathf.Clamp( gas + objectManager.Collide(), 0, 100);
            Debug.Log(gas);
            if (gas == 0)
            {
                engineOn = true;
                gas = 100;
            } 
        }
        if (objectManager == null)
        {
            Debug.Log("Ei IObjectManageria");
        }
    }  

    public bool EngineOn
    {
        set { engineOn = value; }
    }
    public void Stop()
    {
        rBody.velocity = transform.forward * 0;
        engineOn = false;
    }
}
