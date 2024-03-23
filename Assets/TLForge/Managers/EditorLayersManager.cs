#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Static class that handles Layers features during Editor mode
/// </summary>
public static class EditorLayerManager
{
    #region METHODS

    #region ADD LAYERS
    public static void AddLayers(string[] layersNames)
    {
        for (int i = 0; i < layersNames.Length; i++)
        {
            AddLayer(layersNames[i]);
        }
    }
    public static void AddLayer(string layerName)
    {
        if (string.IsNullOrEmpty(layerName) || LayerExists(layerName))
        {
            Debug.LogWarning("Layer already exists or layer name is empty.");
            return;
        }

        SerializedObject tagManager = GetTagManagerSerializedObject();
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        for (int i = 8; i < layersProp.arraySize; i++) // Start from 8 to skip Unity's internal layers
        {
            SerializedProperty layerProp = layersProp.GetArrayElementAtIndex(i);
            if (layerProp.stringValue == "")
            {
                layerProp.stringValue = layerName;
                tagManager.ApplyModifiedProperties();
                return;
            }
        }

        Debug.LogError("No more empty layer slots available.");
    }
    #endregion

    #region DELETE LAYERS
    public static void DeleteLayers(string[] layersNames)
    {
        for (int i = 0; i < layersNames.Length; i++)
        {
            DeleteLayer(layersNames[i]);
        }
    }
    public static void DeleteLayer(string layerName)
    {
        if (string.IsNullOrEmpty(layerName) || !LayerExists(layerName))
        {
            Debug.LogWarning("Layer does not exist or layer name is empty.");
            return;
        }

        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex < 0)
        {
            Debug.LogError($"Layer {layerName} not found.");
            return;
        }

        // Find all GameObjects in the current scene and change their layer if necessary
        Scene currentScene = SceneManager.GetActiveScene();
        if (!currentScene.isLoaded)
        {
            Debug.LogWarning("Current scene is not fully loaded.");
            return;
        }
        GameObject[] rootObjects = currentScene.GetRootGameObjects();
        foreach (GameObject rootObj in rootObjects)
        {
            SetLayerRecursively(rootObj, layerIndex, 0); // 0 means "Default" layer
        }

        SerializedObject tagManager = GetTagManagerSerializedObject();
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        for (int i = 8; i < layersProp.arraySize; i++) // Start from 8 to skip Unity's internal layers
        {
            SerializedProperty layerProp = layersProp.GetArrayElementAtIndex(i);
            if (layerProp.stringValue == layerName)
            {
                layerProp.stringValue = "";
                tagManager.ApplyModifiedProperties();
                return;
            }
        }
    }
    public static void DeleteLayer(GameObject obj, string layerName)
    {
        // In case the Target GameObject is in the custom Layer
        if (IsGameObjectInLayer(obj, layerName))
        {
            // Target will be on Default Layer
            SetLayerByIndex(obj, 0);
        }
        // Custom Layer gets deleted
        DeleteLayer(layerName);
    }
    #endregion

    #region CHECK EXISTENCE
    public static bool IsGameObjectInLayer(GameObject obj, string layerName)
    {
        if(obj == null)
        {
            Debug.LogError("The GameObject provided is null!");
            return false;
        }

        int layer = LayerMask.NameToLayer(layerName);

        return obj.layer == layer;
    }
    public static bool LayerExists(string layerName)
    {
        SerializedObject tagManager = GetTagManagerSerializedObject();
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        for (int i = 0; i < layersProp.arraySize; i++)
        {
            SerializedProperty layerProp = layersProp.GetArrayElementAtIndex(i);
            if (layerProp.stringValue == layerName)
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region SET UP LAYERS
    public static void SetLayerByIndex(GameObject gameObject, int layerIndex)
    {
        if (layerIndex < 0 || layerIndex >= 32)
        {
            Debug.LogError("Invalid layer index. Layer index must be between 0 and 31.");
            return;
        }

        gameObject.layer = layerIndex;
    }
    private static void SetLayerRecursively(GameObject obj, int oldLayerIndex, int newLayerIndex)
    {
        if (obj.layer == oldLayerIndex)
        {
            SetLayerByIndex(obj, newLayerIndex);
        }

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, oldLayerIndex, newLayerIndex);
        }
    }
    #endregion

    private static SerializedObject GetTagManagerSerializedObject()
    {
        return new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
    }

    #endregion
}
#endif