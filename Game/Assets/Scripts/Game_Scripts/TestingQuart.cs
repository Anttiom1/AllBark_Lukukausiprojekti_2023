using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingQuart : MonoBehaviour
{
    private Rigidbody rbody;
    private CustomController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        controller = FindAnyObjectByType<CustomController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, (controller.XQuart) * 10 * Time.deltaTime);
    }
}
