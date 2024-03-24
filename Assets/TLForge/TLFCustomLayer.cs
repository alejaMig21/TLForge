using UnityEngine;

/// <summary>
/// Class that generates and removes Layers faster and easier.
/// </summary>
#if UNITY_EDITOR
[RequireComponent(typeof(TLFLayerCollisionMatrix2D), typeof(TLFLayerCollisionMatrix3D))]
#endif
public class TLFCustomLayer : TLFCustomProjectSetting, ICustomLayer
{
    #region FIELDS
    [SerializeField]
    private string layerName = "NewLayer";
    [SerializeField, HideInInspector]
    private bool loadLayer = false;
    [SerializeField, HideInInspector]
    private bool createCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool applyCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool create_ApplyCustomLayer = false;
    [SerializeField, HideInInspector]
    private bool deleteCustomLayer = false;
    #endregion

    #region PROPERTIES
    public bool LoadLayer { get => loadLayer; set => loadLayer = value; }
    public string LayerName { get => layerName; set => layerName = value; }
    public bool CreateCustomLayer { set => createCustomLayer = value; get => createCustomLayer; }
    public bool ApplyCustomLayer { get => applyCustomLayer; set => applyCustomLayer = value; }
    public bool Create_ApplyCustomLayer { get => create_ApplyCustomLayer; set => create_ApplyCustomLayer = value; }
    public bool DeleteCustomLayer { get => deleteCustomLayer; set => deleteCustomLayer = value; }
    #endregion

    #region METHODS
    void OnValidate()
    {
        CheckStatus();
    }
    public override void CheckStatus()
    {
        // editor
        LoadCurrentValues();

        CustomizeLayer();
        DeleteCustomizedLayer();
        // editor

        ApplyCustomizedLayer();
        CustomizeThenApplyLayer();
    }
    /// <summary>
    /// Editor method
    /// </summary>
    public override void LoadCurrentValues()
    {
#if UNITY_EDITOR
        if (LoadLayer)
        {
            LayerName = LayerMask.LayerToName(Target.layer);

            LoadLayer = false;
        }
#endif
    }
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
                gameObject.layer = layerIndex;
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
#endif
            ApplyCustomLayer = true;
            ApplyCustomizedLayer();
            Create_ApplyCustomLayer = false;
        }
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
}