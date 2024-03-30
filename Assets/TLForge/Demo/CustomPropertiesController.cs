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
    private Button newPresetButton;
    [SerializeField]
    private Button originalPresetButton;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private TLFCustomProperties customProperties;
    [SerializeField]
    private TLFCollisionMatrix2D matrix2D;
    [SerializeField]
    private TLFCollisionMatrix3D matrix3D;
    #endregion

    #region PROPERTIES
    public Button NewTagButton { get => newTagButton; set => newTagButton = value; }
    public Button OriginalTagButton { get => originalTagButton; set => originalTagButton = value; }
    public Button NewLayerButton { get => newLayerButton; set => newLayerButton = value; }
    public Button OriginalLayerButton { get => originalLayerButton; set => originalLayerButton = value; }
    public GameObject TargetObject { get => targetObject; set => targetObject = value; }
    public TLFCustomProperties CustomProperties { get => customProperties; set => customProperties = value; }
    public Button NewPresetButton { get => newPresetButton; set => newPresetButton = value; }
    public Button OriginalPresetButton { get => originalPresetButton; set => originalPresetButton = value; }
    public TLFCollisionMatrix2D Matrix2D { get => matrix2D; set => matrix2D = value; }
    public TLFCollisionMatrix3D Matrix3D { get => matrix3D; set => matrix3D = value; }
    #endregion

    #region METHODS
    void Start()
    {
        if (TargetObject == null || CustomProperties == null)
        {
            Debug.LogError("Target object or CustomProperties not set.");
            return;
        }

        NewTagButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyTag(TargetObject, "TLForgeDemoTag"));
        NewLayerButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyLayer(TargetObject, "TLForgeDemoLayer"));
        NewPresetButton.onClick.AddListener(() => Matrix2D.Import("EditorData/CollisionData/2D/", "TLForgeDemoMatrix2D_Alt"));
        NewPresetButton.onClick.AddListener(() => Matrix3D.Import("EditorData/CollisionData/3D/", "TLForgeDemoMatrix3D_Alt"));

        OriginalTagButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyTag(TargetObject, "Untagged"));
        OriginalLayerButton.onClick.AddListener(() => CustomProperties.CustomizeThenApplyLayer(TargetObject, "Default"));
        OriginalPresetButton.onClick.AddListener(() => Matrix2D.Import("EditorData/CollisionData/2D/", "TLForgeDemoMatrix2D"));
        OriginalPresetButton.onClick.AddListener(() => Matrix3D.Import("EditorData/CollisionData/3D/", "TLForgeDemoMatrix3D"));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Matrix2D.SetCollision(5, 8, true);
            Matrix2D.SetCollision(0, 8, true);
            Matrix2D.ApplyData();
            Matrix3D.SetCollision(5, 8, true);
            Matrix3D.SetCollision(0, 8, true);
            Matrix3D.ApplyData();
        }
    }
#endregion
}