using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{
    public List<GameObject> prefabsList = new List<GameObject>();
    public int numberOfGenerations;

    private GameObject boundObj;


    void Start()
    {
        boundObj = GetComponent<GameObject>();
        SpawnTrees();
    }

    void SpawnTrees()
    {
        if (prefabsList.Count == 0)
        {
            Debug.LogError("Prefabs list is empty. Please assign prefabs in the Inspector.");
            return;
        }

        Bounds bounds = boundObj.GetComponent<Renderer>().bounds;

        for (int i = 0; i < numberOfGenerations; i++) // You can adjust the number of trees to spawn
        {
            GameObject randomTreePrefab = prefabsList[Random.Range(0, prefabsList.Count)];

            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                0,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            Instantiate(randomTreePrefab, randomPosition, Quaternion.identity);
        }


    }
}
