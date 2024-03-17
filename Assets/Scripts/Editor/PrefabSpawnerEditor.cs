using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoundManager))]
public class PrefabSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BoundManager prefabSpawner = (BoundManager)target;

        if (GUILayout.Button("Spawn Prefabs in Editor"))
        {
            prefabSpawner.SpawnPrefabs();
        }
    }
}
