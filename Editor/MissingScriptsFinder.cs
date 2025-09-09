using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MissingScriptsFinder : EditorWindow
{
    private List<GameObject> objectsWithMissingScripts = new List<GameObject>();
    private Vector2 scrollPosition;
    private bool isScanning = false;
    private int totalMissingScripts = 0;

    [MenuItem("Tools/Zero/Open Missing Scripts Finder")]
    public static void ShowWindow()
    {
        MissingScriptsFinder window = GetWindow<MissingScriptsFinder>("Missing Scripts Finder");
        window.minSize = new Vector2(400, 300);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Missing Scripts Finder", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Scan button
        if (GUILayout.Button("Scan for Missing Scripts", GUILayout.Height(30)))
        {
            ScanForMissingScripts();
        }

        EditorGUILayout.Space();

        // Display scan results
        if (objectsWithMissingScripts.Count > 0)
        {
            EditorGUILayout.LabelField($"Found {objectsWithMissingScripts.Count} objects with {totalMissingScripts} missing scripts:", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Auto Fix button
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Auto Fix - Remove All Missing Scripts", GUILayout.Height(40)))
            {
                if (EditorUtility.DisplayDialog("Confirm Auto Fix", 
                    $"This will remove {totalMissingScripts} missing script components from {objectsWithMissingScripts.Count} objects. This action cannot be undone.\n\nDo you want to continue?", 
                    "Yes, Remove Missing Scripts", "Cancel"))
                {
                    AutoFixMissingScripts();
                }
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.Space();

            // Scrollable list of objects
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            for (int i = 0; i < objectsWithMissingScripts.Count; i++)
            {
                GameObject obj = objectsWithMissingScripts[i];
                if (obj == null)
                {
                    EditorGUILayout.LabelField($"[Deleted Object]", EditorStyles.miniLabel);
                    continue;
                }

                EditorGUILayout.BeginHorizontal();
                
                // Object field (clickable)
                EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                
                // Count missing scripts on this object
                int missingCount = GetMissingScriptCount(obj);
                EditorGUILayout.LabelField($"({missingCount} missing)", GUILayout.Width(80));
                
                // Individual fix button
                if (GUILayout.Button("Fix", GUILayout.Width(40)))
                {
                    if (EditorUtility.DisplayDialog("Confirm Fix", 
                        $"Remove {missingCount} missing script(s) from '{obj.name}'?", 
                        "Yes", "No"))
                    {
                        RemoveMissingScriptsFromObject(obj);
                        // Refresh the list
                        ScanForMissingScripts();
                    }
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndScrollView();
        }
        else if (!isScanning)
        {
            EditorGUILayout.LabelField("No missing scripts found. Click 'Scan' to search for missing scripts.");
        }

        if (isScanning)
        {
            EditorGUILayout.LabelField("Scanning...", EditorStyles.boldLabel);
        }
    }

    private void ScanForMissingScripts()
    {
        isScanning = true;
        objectsWithMissingScripts.Clear();
        totalMissingScripts = 0;

        // Find all GameObjects in the current scene hierarchy only
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            // Only include objects that are in the scene hierarchy (not prefab assets)
            if (obj.scene.IsValid() && HasMissingScripts(obj))
            {
                objectsWithMissingScripts.Add(obj);
                totalMissingScripts += GetMissingScriptCount(obj);
            }
        }

        isScanning = false;
        Repaint();
    }

    private bool HasMissingScripts(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        return components.Any(component => component == null);
    }

    private int GetMissingScriptCount(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        return components.Count(component => component == null);
    }

    private void AutoFixMissingScripts()
    {
        int fixedCount = 0;
        int fixedObjects = 0;

        // Create undo group
        Undo.SetCurrentGroupName("Remove Missing Scripts");
        int undoGroup = Undo.GetCurrentGroup();

        foreach (GameObject obj in objectsWithMissingScripts.ToList())
        {
            if (obj != null)
            {
                int removedCount = RemoveMissingScriptsFromObject(obj);
                if (removedCount > 0)
                {
                    fixedCount += removedCount;
                    fixedObjects++;
                }
            }
        }

        Undo.CollapseUndoOperations(undoGroup);

        // Show result dialog
        EditorUtility.DisplayDialog("Auto Fix Complete", 
            $"Removed {fixedCount} missing script components from {fixedObjects} objects.", "OK");

        // Refresh the scan
        ScanForMissingScripts();
    }

    private int RemoveMissingScriptsFromObject(GameObject obj)
    {
        if (obj == null) return 0;

        // Register undo
        Undo.RegisterCompleteObjectUndo(obj, "Remove Missing Scripts");

        Component[] components = obj.GetComponents<Component>();
        int removedCount = 0;

        // Remove missing scripts (null components)
        for (int i = components.Length - 1; i >= 0; i--)
        {
            if (components[i] == null)
            {
                // For prefabs, we need to handle them differently
                if (PrefabUtility.IsPartOfPrefabAsset(obj))
                {
                    // Mark prefab as dirty
                    EditorUtility.SetDirty(obj);
                }
                
                // Remove the missing script
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(obj);
                removedCount++;
                break; // RemoveMonoBehavioursWithMissingScript removes all missing scripts at once
            }
        }

        return removedCount;
    }

    private void OnInspectorUpdate()
    {
        // Repaint the window periodically to update the UI
        if (isScanning)
        {
            Repaint();
        }
    }
}
