using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;


/// <summary>
/// Class that handles the main methods for importing ScriptableObjects
/// </summary>
public static class DirectoryManager
{
    #region METHODS
    public static void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    public static void SaveScriptableObject(string path, ScriptableObject obj)
    {
        CreatePath(path);

        AssetDatabase.CreateAsset(obj, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    public static T LoadScriptableObject<T>(string path) where T : ScriptableObject
    {
        T loadedObject = AssetDatabase.LoadAssetAtPath<T>(path);

        if (loadedObject == null)
        {
            Debug.LogError("Unable to load ScriptableObject on route: " + path);
        }

        return loadedObject;
    }
    #endregion
}
#endif