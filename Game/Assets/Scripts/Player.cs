using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IController
{
    protected Rigidbody rBody;

    protected Vector3 movementVector;

    protected float movementSpeed;
    protected float rotateSpeed;

    protected bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        movementSpeed = 10;
        rotateSpeed = 75;

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }

    private void FixedUpdate()
    {
        rBody.AddForce(movementVector);
    }
    protected virtual void Init()
    {
        rBody = GetComponent<Rigidbody>();
        if (null == rBody)
        {
            this.gameObject.SetActive(false);
        }

        Controller myController = new(this);
    }

    public virtual void Axis(float horizontalValue, float vericalValue)
    {
        transform.Rotate(Vector3.up, (horizontalValue) * rotateSpeed * Time.deltaTime);
        if (vericalValue > 0)
        {
            moving = true;
        }
        
    }
    
   

}
