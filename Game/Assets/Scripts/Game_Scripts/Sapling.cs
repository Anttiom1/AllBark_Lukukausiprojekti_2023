using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour, IObjectManager
{
    public void Collide()
    {
        GameManager.instance.AddScore(-1);
        Destroy(gameObject);
    }


}
