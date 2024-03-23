using UnityEngine;
using UnityEditor;

public class CustomPropertiesMenu : EditorWindow
{
    #region FIELDS
    private string tagName = "NewTag";
    private string layerName = "NewLayer";
    private GameObject selectedObj;
    #endregion

    #region PROPERTIES
    public string TagName { get => tagName; set => tagName = value; }
    public string LayerName { get => layerName; set => layerName = value; }
    public GameObject SelectedObj { get => selectedObj; set => selectedObj = value; }
    #endregion

    #region METHODS
    [MenuItem("GameObject/TL -QUICK- Forge", false, 0)]
    static void Init()
    {
        CustomPropertiesMenu window = (CustomPropertiesMenu)EditorWindow.GetWindow(typeof(CustomPropertiesMenu), true, "CUSTOMIZE PROPERTIES");
        window.Show();
        window.SelectedObj = Selection.activeGameObject;
    }

    void OnGUI()
    {
        //GUILayout.Label("Customize Properties", EditorStyles.boldLabel);

        if (SelectedObj != null)
        {
            GUILayout.Label("Selected Object: " + SelectedObj.name);
        }
        else
        {
            GUILayout.Label("No object selected!");
        }

        if(TagName == string.Empty)
        {
            TagName = "DefaultTag";
        }
        if (LayerName == string.Empty)
        {
            LayerName = "DefaultLayer";
        }

        SetGUIEnabledState(SelectedObj != null);

        TagName = EditorGUILayout.TextField("Tag Name", TagName);
        LayerName = EditorGUILayout.TextField("Layer Name", LayerName);

        GUILayout.Space(10);

        if (GUILayout.Button("CREATE CUSTOM TAG"))
        {
            EditorTagManager.AddTag(TagName);
        }

        GUILayout.Space(5);

        if (GUILayout.Button("APPLY CUSTOM TAG"))
        {
            SelectedObj.tag = tagName;
        }

        GUILayout.Space(5);

        if (GUILayout.Button("CREATE & APPLY CUSTOM TAG"))
        {
            EditorTagManager.AddTag(TagName);
            SelectedObj.tag = tagName;
        }

        GUILayout.Space(5);

        if (GUILayout.Button("DELETE CUSTOM TAG"))
        {
            EditorTagManager.DeleteTag(SelectedObj, TagName);
        }

        GUILayout.Space(5);

        if (GUILayout.Button("CREATE CUSTOM LAYER"))
        {
            EditorLayerManager.AddLayer(LayerName);
        }

        GUILayout.Space(5);

        if (GUILayout.Button("APPLY CUSTOM LAYER"))
        {
            int layerIndex = RuntimeLayersManager.GetLayerIndex(LayerName);
            if (layerIndex != -1)
            {
                SelectedObj.layer = layerIndex;
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("CREATE & APPLY CUSTOM LAYER"))
        {
            EditorLayerManager.AddLayer(LayerName);
            int layerIndex = RuntimeLayersManager.GetLayerIndex(LayerName);
            if (layerIndex != -1)
            {
                SelectedObj.layer = layerIndex;
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("DELETE CUSTOM LAYER"))
        {
            EditorLayerManager.DeleteLayer(SelectedObj, LayerName);
        }

        GUILayout.Space(10);
    }
    void OnSelectionChange()
    {
        UpdateSelection();
        Repaint(); // Asegura que la ventana se actualice con la nueva selección
    }
    private void UpdateSelection()
    {
        SelectedObj = Selection.activeGameObject;
    }
    private void SetGUIEnabledState(bool state)
    {
        GUI.enabled = state;
    }
    #endregion
}