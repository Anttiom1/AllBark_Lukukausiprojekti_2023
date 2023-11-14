using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTree : MonoBehaviour, IObjectManager
{
    public GameObject Stump;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void Collide()
    {
        Vector3 position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        Instantiate(Stump, position , Quaternion.identity);
        Destroy(gameObject);

    }
}
