//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("SpawnLocations")]
    public Transform spawnLocation1;
    public Transform spawnLocation2;

    [Header("SpawnTimer")][SerializeField] float timeSpawn;

    public GameObject[] objectsToSpawn;
    bool canInstantiate;
    int currentRandom;
    [SerializeField] Transform currentSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        canInstantiate = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentRandom = Random.Range(0, 10);
        if (currentRandom > 5)
        {
            currentSpawnPosition = spawnLocation1;
        }
        else
        {
            currentSpawnPosition = spawnLocation2;
        }
        foreach (var gameObject in objectsToSpawn)
        {
            if (canInstantiate == true)
            {
                StartCoroutine("spawnObject", gameObject);
                canInstantiate = false;
            }
            else
            {
                return;
            }

        }

    }

    IEnumerator spawnObject(GameObject objectSpawn)
    {
        Instantiate(objectSpawn, currentSpawnPosition.position, currentSpawnPosition.rotation);
        yield return new WaitForSeconds(timeSpawn);
        canInstantiate = true;
    }

}
