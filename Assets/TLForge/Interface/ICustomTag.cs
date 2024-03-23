/// <summary>
/// Interface to implement ways to customize Tags
/// </summary>
public interface ICustomTag
{
    public string TagName { set; get; }
    public bool CreateCustomTag { set; get; }
    public bool ApplyCustomTag { set; get; }
    public bool Create_ApplyCustomTag { set; get; }
    public bool DeleteCustomTag { set; get; }
    public void CustomizeTag();
    public void ApplyCustomizedTag();
    public void CustomizeThenApplyTag();
    public void DeleteCustomizedTag();
}