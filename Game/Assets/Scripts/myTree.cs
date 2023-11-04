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
    public void Collide()
    {
        Destroy(gameObject);
        Vector3 position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        Instantiate(Stump, position , Quaternion.identity);
        GameManager.instance.AddScore(1); // Yksi piste per puu, vaihtaa myöhemmin
        Destroy(gameObject);
    }
}
