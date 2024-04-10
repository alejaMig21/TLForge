using UnityEngine;

[ExecuteInEditMode]
public class DemoValues : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private TLFCustomProperties tlf = null;
    #endregion

    #region PROPERTIES
    public TLFCustomProperties Tlf { get => tlf; set => tlf = value; }
    #endregion

    #region METHODS
    private void Start()
    {
        if(Tlf != null)
        {
            Tlf.CreateCustomTag = true;
            Tlf.CustomizeTag();

            Tlf.CreateCustomLayer = true;
            Tlf.CustomizeLayer();
        }
    }
    #endregion
}