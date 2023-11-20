using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCannister : MonoBehaviour, IObjectManager
{
    public float Collide()
    {
        Destroy(gameObject);
        return 100;
    }
}
