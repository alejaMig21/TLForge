using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CreateTLFTools
{
    [MenuItem("GameObject/TLForge/2D Collision Manager", false, 1)]
    private static void CreateCollisionManager2D()
    {
        CreateGameObject<TLFCollisionMatrix2D>("2DCollisionManager");
    }

    [MenuItem("GameObject/TLForge/3D Collision Manager", false, 2)]
    private static void CreateCollisionManager3D()
    {
        CreateGameObject<TLFCollisionMatrix3D>("3DCollisionManager");
    }

    [MenuItem("GameObject/TLForge/Forge", false, 0)]
    private static void CreateForge()
    {
        GameObject forge = CreateGameObject<TLFCustomProperties>("TLForge");
        forge.AddComponent<TLFCollisionMatrix2D>();
        forge.AddComponent<TLFCollisionMatrix3D>();
    }

    private static GameObject CreateGameObject<T>(string name) where T : MonoBehaviour
    {
        GameObject newGO = new(name);

        newGO.AddComponent<T>();

        // Mark the scene as dirty to ensure changes need to be saved
        Undo.RegisterCreatedObjectUndo(newGO, $"Create {name}");
        EditorSceneManager.MarkSceneDirty(newGO.scene);

        // Optional: Select and focus the created GameObject in the hierarchy
        Selection.activeGameObject = newGO;
        EditorGUIUtility.PingObject(newGO);

        return newGO;
    }
}