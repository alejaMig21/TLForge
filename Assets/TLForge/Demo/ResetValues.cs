using UnityEngine;
using UnityEngine.UI;

public class ResetValues : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private Button resetButton;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private TLFCustomProperties customProperties;
    private Vector3 initialPosition = Vector3.zero;
    [SerializeField]
    private Rigidbody2D rBody2D = null;
    [SerializeField]
    private Rigidbody rBody3D = null;
    #endregion

    #region PROPERTIES
    public Button ResetButton { get => resetButton; set => resetButton = value; }
    public GameObject TargetObject { get => targetObject; set => targetObject = value; }
    public TLFCustomProperties CustomProperties { get => customProperties; set => customProperties = value; }
    public Vector3 InitialPosition { get => initialPosition; set => initialPosition = value; }
    public Rigidbody2D RBody2D { get => rBody2D; set => rBody2D = value; }
    public Rigidbody RBody3D { get => rBody3D; set => rBody3D = value; }
    #endregion

    #region METHODS
    void Start()
    {
        if (TargetObject == null || CustomProperties == null)
        {
            Debug.LogError("Target object or CustomProperties not set.");
            return;
        }

        InitialPosition = TargetObject.transform.position;
        ResetButton.onClick.AddListener(ResetProperties);
    }
    public void ResetProperties()
    {
        CustomProperties.CustomizeThenApplyTag("Untagged");
        CustomProperties.CustomizeThenApplyLayer("Default");
        TargetObject.transform.position = InitialPosition;
        if(rBody2D != null)
        {
            rBody2D.velocity = Vector3.zero;
        }
        if (rBody3D != null)
        {
            rBody3D.velocity = Vector3.zero;
        }
    }
    #endregion
}