using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectManager
{
    void Collide();
}

public class ObjectManager : MonoBehaviour
{
    public GameObject[] myObjects;
    private void Start()
    {
        int amountOfTrees = 0;
        while (amountOfTrees < 25)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPoint = new Vector3(Random.Range(-20, 20), 2.3f, (Random.Range(-20, 20)));
            Instantiate(myObjects[0], randomSpawnPoint, Quaternion.identity);
            amountOfTrees++;
        }
        int amountOfStones = 0;
        while (amountOfStones < 10)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPoint = new Vector3(Random.Range(-20, 20), 0, (Random.Range(-20, 20)));
            Instantiate(myObjects[1], randomSpawnPoint, Quaternion.identity);
            amountOfStones++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
