using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class CreateCollisionManager
{
    [MenuItem("GameObject/TLForge/CollisionManager2D", false, 1)]
    private static void CreateCollisionManager2D()
    {
        CreateGameObject<TLFLayerCollisionMatrix2D>("CollisionManager2D");
    }

    [MenuItem("GameObject/TLForge/CollisionManager3D", false, 2)]
    private static void CreateCollisionManager3D()
    {
        CreateGameObject<TLFLayerCollisionMatrix3D>("CollisionManager3D");
    }

    [MenuItem("GameObject/TLForge/Forge", false, 0)]
    private static void CreateForge()
    {
        GameObject forge = CreateGameObject<TLFCustomProperties>("TLForge");
        forge.AddComponent<TLFLayerCollisionMatrix2D>();
        forge.AddComponent<TLFLayerCollisionMatrix3D>();
    }

    private static GameObject CreateGameObject<T>(string name) where T : MonoBehaviour
    {
        GameObject newGO = new(name);

        newGO.AddComponent<T>();

        // Optional: Select and focus the created GameObject in the hierarchy
        Selection.activeGameObject = newGO;
        EditorGUIUtility.PingObject(newGO);

        return newGO;
    }
}