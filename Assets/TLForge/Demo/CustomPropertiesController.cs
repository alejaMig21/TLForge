using UnityEngine;
using UnityEngine.UI;

public class CustomPropertiesController : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private Button newTagButton;
    [SerializeField]
    private Button originalTagButton;
    [SerializeField]
    private Button newLayerButton;
    [SerializeField]
    private Button originalLayerButton;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private CustomProperties customProperties;
    #endregion

    #region PROPERTIES
    public Button NewTagButton { get => newTagButton; set => newTagButton = value; }
    public Button OriginalTagButton { get => originalTagButton; set => originalTagButton = value; }
    public Button NewLayerButton { get => newLayerButton; set => newLayerButton = value; }
    public Button OriginalLayerButton { get => originalLayerButton; set => originalLayerButton = value; }
    public GameObject TargetObject { get => targetObject; set => targetObject = value; }
    public CustomProperties CustomProperties { get => customProperties; set => customProperties = value; }
    #endregion

    #region METHODS
    void Start()
    {
        if (TargetObject == null || CustomProperties == null)
        {
            Debug.LogError("Target object or CustomProperties not set.");
            return;
        }

        NewTagButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyTag("NewTag"));
        NewLayerButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyLayer("NewLayer"));
        OriginalTagButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyTag("Untagged"));
        OriginalLayerButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyLayer("Default"));
    }
    #endregion
}