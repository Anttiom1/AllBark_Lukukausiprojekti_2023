using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCannister : MonoBehaviour, IObjectManager
{
    public void Collide()
    {

        Destroy(gameObject);
    }
}
