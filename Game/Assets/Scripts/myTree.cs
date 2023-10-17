using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tree";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collide()
    {
        Destroy(gameObject);
    }
}
