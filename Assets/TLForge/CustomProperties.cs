using UnityEditor;
using UnityEngine;

/// <summary>
/// Class to customize Tags & Layers
/// </summary>
public class CustomProperties : CustomProjectSetting, ICustomTag, ICustomLayer
{
    #region FIELDS

    #region TAG
    [SerializeField]
    private string tagName = "NewTag";
    [SerializeField, HideInInspector]
    private bool createCustomTag = false;
    [SerializeField, HideInInspector]
    private bool applyCustomTag = false;
    [SerializeField, HideInInspector]
    private bool create_ApplyCustomTag = false;
    [SerializeField, HideInInspector]
    private bool deleteCustomTag = false;
    #endregion

    #region LAYER
    [SerializeField]
    private string layerName = "NewLayer";
    [SerializeField, HideInInspector]
    private bool createCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool applyCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool create_ApplyCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool deleteCustomLayer = false;
    #endregion

    #endregion

    #region PROPERTIES

    #region TAG
    public string TagName { get => tagName; set => tagName = value; }
    public bool CreateCustomTag { set => createCustomTag = value; get => createCustomTag; }
    public bool ApplyCustomTag { get => applyCustomTag; set => applyCustomTag = value; }
    public bool Create_ApplyCustomTag { get => create_ApplyCustomTag; set => create_ApplyCustomTag = value; }
    public bool DeleteCustomTag { get => deleteCustomTag; set => deleteCustomTag = value; }
    #endregion

    #region LAYER
    public string LayerName { get => layerName; set => layerName = value; }
    public bool CreateCustomLayer { set => createCustomLayer = value; get => createCustomLayer; }
    public bool ApplyCustomLayer { get => applyCustomLayer; set => applyCustomLayer = value; }
    public bool Create_ApplyCustomLayer { get => create_ApplyCustomLayer; set => create_ApplyCustomLayer = value; }
    public bool DeleteCustomLayer { get => deleteCustomLayer; set => deleteCustomLayer = value; }
    #endregion

    #endregion

    #region METHODS
    void OnValidate()
    {
        if(Target == null)
        {
            Target = gameObject;
        }
        CheckStatus();
    }
    public override void CheckStatus()
    {
#if UNITY_EDITOR
        CustomizeTag();
        DeleteCustomizedTag();
#endif
        ApplyCustomizedTag();
        CustomizeThenApplyTag();

#if UNITY_EDITOR
        CustomizeLayer();
        DeleteCustomizedLayer();
#endif
        ApplyCustomizedLayer();
        CustomizeThenApplyLayer();
    }

    #region TAG
    public void CustomizeTag()
    {
#if UNITY_EDITOR
        if (CreateCustomTag)
        {
            EditorTagManager.AddTag(TagName);
            CreateCustomTag = false;
        }
#endif
    }
    public void ApplyCustomizedTag()
    {
        if (ApplyCustomTag)
        {
            Target.tag = tagName;

            ApplyCustomTag = false;
        }
    }
    public void CustomizeThenApplyTag()
    {
        if (Create_ApplyCustomTag)
        {
#if UNITY_EDITOR
            CreateCustomTag = true;
            CustomizeTag();
            EditorGUIUtility.PingObject(Target);
#endif
            ApplyCustomTag = true;
            ApplyCustomizedTag();
            Create_ApplyCustomTag = false;
        }
    }
    public void CustomizeThenApplyTag(string newTag)
    {
        TagName = newTag;
        Create_ApplyCustomTag = true;

        CustomizeThenApplyTag();
    }
    public void DeleteCustomizedTag()
    {
#if UNITY_EDITOR
        if (DeleteCustomTag)
        {
            // In case the Target GameObject has the custom Tag
            if (EditorTagManager.DoesGameObjectHasTag(Target, TagName))
            {
                // Target will be Untagged
                EditorTagManager.SetTagByIndex(Target, 0);
            }
            // Custom Tag gets deleted
            EditorTagManager.DeleteTag(TagName);
            DeleteCustomTag = false;
        }
#endif
    }
    #endregion

    #region LAYER
    public void CustomizeLayer()
    {
#if UNITY_EDITOR
        if (CreateCustomLayer)
        {
            EditorLayerManager.AddLayer(LayerName);
            CreateCustomLayer = false;
        }
#endif
    }
    public void ApplyCustomizedLayer()
    {
        if (ApplyCustomLayer)
        {
            int layerIndex = RuntimeLayersManager.GetLayerIndex(LayerName);
            if (layerIndex != -1)
            {
                Target.layer = layerIndex;
            }
            else
            {
                Debug.LogError("Layer not found: " + LayerName);
            }

            ApplyCustomLayer = false;
        }
    }
    public void CustomizeThenApplyLayer()
    {
        if (Create_ApplyCustomLayer)
        {
#if UNITY_EDITOR
            CreateCustomLayer = true;
            CustomizeLayer();
            EditorGUIUtility.PingObject(Target);
#endif
            ApplyCustomLayer = true;
            ApplyCustomizedLayer();
            Create_ApplyCustomLayer = false;
        }
    }
    public void CustomizeThenApplyLayer(string newLayer)
    {
        LayerName = newLayer;
        Create_ApplyCustomLayer = true;

        CustomizeThenApplyLayer();
    }
    public void DeleteCustomizedLayer()
    {
#if UNITY_EDITOR
        if (DeleteCustomLayer)
        {
            // In case the Target GameObject is in the custom Layer
            if (EditorLayerManager.IsGameObjectInLayer(Target, LayerName))
            {
                // Target will be on Default Layer
                EditorLayerManager.SetLayerByIndex(Target, 0);
            }
            // Custom Layer gets deleted
            EditorLayerManager.DeleteLayer(LayerName);
            DeleteCustomLayer = false;
        }
#endif
    }
    #endregion

    #endregion
}