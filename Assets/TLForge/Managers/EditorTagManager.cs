#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Static class that handles Tags features during Editor mode
/// </summary>
public static class EditorTagManager
{
    #region METHODS

    #region ADD TAG
    public static void AddTag(string tagName)
    {
        if (string.IsNullOrEmpty(tagName) || TagExists(tagName))
        {
            Debug.LogWarning("Tag already exists or tag name is empty.");
            return;
        }

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
        SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
        newTagProp.stringValue = tagName;
        tagManager.ApplyModifiedProperties();
    }
    #endregion

    #region DELETE TAG
    public static void DeleteTag(string tagName)
    {
        if (string.IsNullOrEmpty(tagName) || !TagExists(tagName))
        {
            Debug.LogWarning("Tag does not exist or tag name is empty.");
            return;
        }

        // Antes de eliminar la etiqueta, reasigna la etiqueta de todos los GameObjects que la usan
        ReassignTagInAllObjects(tagName, "Untagged");

        SerializedObject tagManager = new(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty tagProp = tagsProp.GetArrayElementAtIndex(i);
            if (tagProp.stringValue == tagName)
            {
                tagsProp.DeleteArrayElementAtIndex(i);
                tagManager.ApplyModifiedProperties();
                return;
            }
        }

        Debug.LogError("Tag not found.");
    }

    public static void DeleteTag(GameObject obj, string tagName)
    {
        // In case the Target GameObject has the custom Tag
        if (DoesGameObjectHasTag(obj, tagName))
        {
            // Target will be Untagged
            SetTagByIndex(obj, 0);
        }
        // Custom Tag gets deleted
        DeleteTag(tagName);
    }
    #endregion

    #region CHECK EXISTENCE
    public static bool DoesGameObjectHasTag(GameObject obj, string tagName)
    {
        if (obj == null)
        {
            Debug.LogError("The GameObject provided is null!");
            return false;
        }

        return obj.CompareTag(tagName);
    }
    public static bool TagExists(string tagName)
    {
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
        foreach (string tag in tags)
        {
            if (tag == tagName)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region SET UP TAGS
    public static void SetTagByIndex(GameObject gameObject, int index)
    {
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
        if (index < 0 || index >= tags.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }

        string tagName = tags[index];
        gameObject.tag = tagName;
    }

    private static void ReassignTagInAllObjects(string oldTag, string newTag)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(oldTag);
        foreach (GameObject obj in allObjects)
        {
            obj.tag = newTag;
        }
    }
    #endregion

    #endregion
}
#endif