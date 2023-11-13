using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TileOcclusion))]
public class TileOcclusionEditor : Editor
{
    SerializedProperty rendererList;

    void OnEnable()
    {
        rendererList = serializedObject.FindProperty("renderers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the default inspector
        DrawDefaultInspector();

        if (GUILayout.Button("Update Renderer List"))
        {
            UpdateRendererList();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void UpdateRendererList()
    {
        TileOcclusion tileOcclusion = (TileOcclusion)target;

        // Clear the existing list
        tileOcclusion.renderers.Clear();

        // Get all Renderers in children
        Renderer[] renderers = tileOcclusion.GetComponentsInChildren<Renderer>(true);

        // Add them to the list
        tileOcclusion.renderers.AddRange(renderers);

        EditorUtility.SetDirty(tileOcclusion);

        // Log the count
        Debug.Log("Updated Renderer List. Count: " + tileOcclusion.renderers.Count);
    }
}
