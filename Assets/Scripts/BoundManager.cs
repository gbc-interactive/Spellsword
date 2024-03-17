using UnityEngine;
using System.Collections.Generic;

public class BoundManager : MonoBehaviour
{
    public GameObject treePrefab;
    public int numberOfPrefabs = 100;
    public float yOffset = 3.0f;
    public float edgePrefabSpacing = 1.5f; // Distance between trees on the edges
    public float edgePrefabVariation = 3f; // Small random variation on the XZ axis for edge trees
    public MeshRenderer meshRenderer;

    public void SpawnPrefabs()
    {
        Bounds bounds = meshRenderer.bounds;

        // Spawn random trees inside the bounds
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 randomPosition = GetRandomPositionInsideBounds(bounds);
            GameObject treeInstance = Instantiate(treePrefab, randomPosition, Quaternion.identity);
            treeInstance.transform.parent = transform;
        }

        // Spawn trees on the edges at regular intervals with small random variations
        SpawnPrefabsOnEdges(bounds);

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    void SpawnPrefabsOnEdges(Bounds bounds)
    {
        bool addOffset;
        int iteration = 0;

        // Spawn on all edges
        for (float t = 0; t <= 1.0f; t += edgePrefabSpacing / bounds.size.x)
        {
            iteration++;
            if (iteration % 2 == 0)
            {
                addOffset = true;
            }
            else
            {
                addOffset = false;
            }

            float x = Mathf.Lerp(bounds.min.x, bounds.max.x, t);
            float z = Mathf.Lerp(bounds.min.z, bounds.max.z, t);

            Vector3 position;

            // Spawn on top edge
            position = new Vector3(x, yOffset, bounds.min.z);
            if (addOffset)
            {
                position = new Vector3(x, yOffset, bounds.min.z + edgePrefabVariation);
            }
            InstantiateRandomPrefabOnEdge(position);

            // Spawn on bottom edge
            position = new Vector3(x, yOffset, bounds.max.z);
            if (addOffset)
            {
                position = new Vector3(x, yOffset, bounds.max.z - edgePrefabVariation);
            }
            InstantiateRandomPrefabOnEdge(position);

            // Spawn on left edge
            position = new Vector3(bounds.min.x, yOffset, z);
            if (addOffset)
            {
                position = new Vector3(bounds.min.x + edgePrefabVariation, yOffset, z);
            }
            InstantiateRandomPrefabOnEdge(position);

            // Spawn on right edge
            position = new Vector3(bounds.max.x, yOffset, z);
            if (addOffset)
            {
                position = new Vector3(bounds.max.x - edgePrefabVariation, yOffset, z);
            }
            InstantiateRandomPrefabOnEdge(position);
        }
    }

    void InstantiateRandomPrefabOnEdge(Vector3 position)
    {
        GameObject treeInstance = Instantiate(treePrefab, position, Quaternion.identity);
        treeInstance.transform.parent = transform;
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
