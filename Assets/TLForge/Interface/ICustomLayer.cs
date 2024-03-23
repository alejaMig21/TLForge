using UnityEngine;

/// <summary>
/// Interface to implement ways to customize Layers
/// </summary>
public interface ICustomLayer
{
    public string LayerName { set; get; }
    public bool CreateCustomLayer { set; get; }
    public bool ApplyCustomLayer { set; get; }
    public bool Create_ApplyCustomLayer { set; get; }
    public bool DeleteCustomLayer { set; get; }
    public void CustomizeLayer();
    public void ApplyCustomizedLayer();
    public void CustomizeThenApplyLayer();
    public void DeleteCustomizedLayer();
}