using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VariationManager))]
public class VariationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VariationManager variationManager = (VariationManager)target;

        if (GUILayout.Button("Run Variations"))
        {
            variationManager.RunVariations();
        }
    }
}
