using UnityEditor;
using UnityEngine;

/// <summary>
/// Class to customize Tags & Layers
/// </summary>
public class TLFCustomProperties : TLFCustomProjectSetting, ICustomTag, ICustomLayer
{
    #region FIELDS

    [SerializeField, HideInInspector]
    private bool loadValues = false;

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

    public bool LoadValues { get => loadValues; set => loadValues = value; }

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
        // editor
        LoadCurrentValues();

        CustomizeTag();
        DeleteCustomizedTag();
        CustomizeLayer();
        DeleteCustomizedLayer();
        // editor

        ApplyCustomizedTag();
        CustomizeThenApplyTag();
        ApplyCustomizedLayer();
        CustomizeThenApplyLayer();
    }

    /// <summary>
    /// Editor method
    /// </summary>
    public override void LoadCurrentValues()
    {
#if UNITY_EDITOR
        if (LoadValues)
        {
            TagName = Target.tag;
            LayerName = LayerMask.LayerToName(Target.layer);

            EditorGUIUtility.PingObject(Target);

            LoadValues = false;
        }
#endif
    }

    #region TAG
    /// <summary>
    /// Editor method
    /// </summary>
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
    /// <summary>
    /// Apply an existing Tag to a given Target.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="tagName"></param>
    public void ApplyCustomizedTag(GameObject target, string tagName)
    {
        if(target != null)
        {
            Target = target;
        }
        if(tagName != string.Empty)
        {
            TagName = tagName;
        }
        ApplyCustomTag = true;

        ApplyCustomizedTag();
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
    /// <summary>
    /// Define a Tag for a Target.
    /// In Editor mode the Tag can be new.
    /// In a Build the Tag needs to exists already.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="newTag"></param>
    public void CustomizeThenApplyTag(GameObject target, string newTag)
    {
        if (target != null)
        {
            Target = target;
        }
        if (newTag != string.Empty)
        {
            TagName = newTag;
        }
        Create_ApplyCustomTag = true;

        CustomizeThenApplyTag();
    }
    /// <summary>
    /// Editor method
    /// </summary>
    public void DeleteCustomizedTag()
    {
#if UNITY_EDITOR
        if (DeleteCustomTag)
        {
            EditorTagManager.DeleteTag(Target, TagName);
            DeleteCustomTag = false;
        }
#endif
    }
    #endregion

    #region LAYER
    /// <summary>
    /// Editor method
    /// </summary>
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
    public void ApplyCustomizedLayer(GameObject target, string layerName)
    {
        if(target != null)
        {
            Target = target;
        }
        if(layerName != string.Empty)
        {
            LayerName = layerName;
        }
        ApplyCustomLayer = true;

        ApplyCustomizedLayer();
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
    public void CustomizeThenApplyLayer(GameObject target, string newLayer)
    {
        if (target != null)
        {
            Target = target;
        }
        if (newLayer != string.Empty)
        {
            LayerName = newLayer;
        }
        Create_ApplyCustomLayer = true;

        CustomizeThenApplyLayer();
    }
    /// <summary>
    /// Editor method
    /// </summary>
    public void DeleteCustomizedLayer()
    {
#if UNITY_EDITOR
        if (DeleteCustomLayer)
        {
            EditorLayerManager.DeleteLayer(Target, LayerName);
            DeleteCustomLayer = false;
        }
#endif
    }
    #endregion

    #endregion
}