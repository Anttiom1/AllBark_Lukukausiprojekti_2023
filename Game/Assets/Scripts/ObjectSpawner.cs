using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects;

    private void Start()
    {
        int amountOfTrees = 0;
        while (amountOfTrees < 25) {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPoint = new Vector3(Random.Range(-20, 20), 2.3f, (Random.Range(-20, 20)));

            Instantiate(myObjects[randomIndex], randomSpawnPoint, Quaternion.identity);
            amountOfTrees++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
