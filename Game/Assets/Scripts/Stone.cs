using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IObjectManager
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Stone";
    }

    public void Collide()
    {
        Debug.Log("kivi");
    }

}
