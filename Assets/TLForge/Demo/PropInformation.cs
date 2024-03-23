using UnityEngine;
using UnityEngine.UI;

public class PropInformation : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private Text currentTag;
    [SerializeField]
    private Text currentLayer;
    [SerializeField]
    private Text currentName;
    #endregion

    #region PROPERTIES
    public Text CurrentTag { get => currentTag; set => currentTag = value; }
    public Text CurrentLayer { get => currentLayer; set => currentLayer = value; }
    public Text CurrentName { get => currentName; set => currentName = value; }
    #endregion

    #region METHODS
    void Start()
    {
        CurrentName.text = "Name: " + gameObject.name;
    }

    void LateUpdate()
    {
        CurrentTag.text = "Tag: " + gameObject.tag;
        CurrentLayer.text = "Layer: " + LayerMask.LayerToName(gameObject.layer);
    }
    #endregion
}