using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public List<GameObject> prefabsList = new List<GameObject>();
    public int numberOfPrefabs = 100;
    public float yOffset = 3.0f;
    public float edgePrefabSpacing = 1.5f; // Distance between trees on the edges
    public float edgePrefabVariation = 1f; // Small random variation on the XZ axis for edge trees

    private MeshRenderer meshRenderer;

    void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        if (prefabsList.Count == 0)
        {
            Debug.LogError("Prefabs list is empty. Please assign prefabs in the Inspector.");
            return;
        }

        Bounds bounds = meshRenderer.bounds;

        // Spawn random trees inside the bounds
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 randomPosition = GetRandomPositionInsideBounds(bounds);

            GameObject randomTreePrefab = prefabsList[Random.Range(0, prefabsList.Count)];

            Instantiate(randomTreePrefab, randomPosition, Quaternion.identity);
        }

        // Spawn trees on the edges at regular intervals with small random variations
        SpawnTreesOnEdges(bounds);

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    void SpawnTreesOnEdges(Bounds bounds)
    {
        // Spawn on top edge
        for (float x = bounds.min.x; x <= bounds.max.x; x += edgePrefabSpacing)
        {
            Vector3 position = new Vector3(x + Random.Range(-edgePrefabVariation, edgePrefabVariation), yOffset, bounds.min.z);
            InstantiateRandomTreeOnEdge(position);
        }

        // Spawn on bottom edge
        for (float x = bounds.min.x; x <= bounds.max.x; x += edgePrefabSpacing)
        {
            Vector3 position = new Vector3(x + Random.Range(-edgePrefabVariation, edgePrefabVariation), yOffset, bounds.max.z);
            InstantiateRandomTreeOnEdge(position);
        }

        // Spawn on left edge
        for (float z = bounds.min.z; z <= bounds.max.z; z += edgePrefabSpacing)
        {
            Vector3 position = new Vector3(bounds.min.x, yOffset, z + Random.Range(-edgePrefabVariation, edgePrefabVariation));
            InstantiateRandomTreeOnEdge(position);
        }

        // Spawn on right edge
        for (float z = bounds.min.z; z <= bounds.max.z; z += edgePrefabSpacing)
        {
            Vector3 position = new Vector3(bounds.max.x, yOffset, z + Random.Range(-edgePrefabVariation, edgePrefabVariation));
            InstantiateRandomTreeOnEdge(position);
        }
    }

    void InstantiateRandomTreeOnEdge(Vector3 position)
    {
        GameObject randomTreePrefab = prefabsList[Random.Range(0, prefabsList.Count)];
        Instantiate(randomTreePrefab, position, Quaternion.identity);
    }

    Vector3 GetRandomPositionInsideBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            yOffset,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
