using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTree : MonoBehaviour, IObjectManager
{
    public GameObject Stump;
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
        Vector3 position = new Vector3 (transform.position.x, 0, transform.position.z);
        Instantiate(Stump, position , Quaternion.identity);
        
    }
}
