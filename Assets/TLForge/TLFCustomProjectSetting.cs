using UnityEngine;

/// <summary>
/// Abstract class intended to customize settings like Tags or Layers
/// </summary>
public abstract class TLFCustomProjectSetting : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private GameObject target = null;
    #endregion

    #region FIELDS
    public GameObject Target { get => target; set => target = value; }
    #endregion

    #region METHODS
    public abstract void CheckStatus();
    /// <summary>
    /// Editor method
    /// </summary>
    public abstract void LoadCurrentValues();
    #endregion
}