using UnityEngine;

/// <summary>
/// Class that generates and removes Tags faster and easier.
/// </summary>
public class TLFCustomTag : TLFCustomProjectSetting, ICustomTag
{
    #region FIELDS
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

    #region PROPERTIES
    public string TagName { get => tagName; set => tagName = value; }
    public bool CreateCustomTag { set => createCustomTag = value; get => createCustomTag; }
    public bool ApplyCustomTag { get => applyCustomTag; set => applyCustomTag = value; }
    public bool Create_ApplyCustomTag { get => create_ApplyCustomTag; set => create_ApplyCustomTag = value; }
    public bool DeleteCustomTag { get => deleteCustomTag; set => deleteCustomTag = value; }
    #endregion

    #region METHODS
    void OnValidate()
    {
        CheckStatus();
    }
    public override void CheckStatus()
    {
#if UNITY_EDITOR
        CustomizeTag();
        DeleteCustomizedTag();
#endif
        ApplyCustomizedTag();
        CustomizeThenApplyTag();
    }
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
            gameObject.tag = tagName;

            ApplyCustomTag = false;
        }
    }
    public void CustomizeThenApplyTag()
    {
        if (Create_ApplyCustomTag)
        {
#if UNITY_EDITOR
            CreateCustomTag = true;
            CustomizeTag();
#endif
            ApplyCustomTag = true;
            ApplyCustomizedTag();
            Create_ApplyCustomTag = false;
        }
    }
    public void DeleteCustomizedTag()
    {
#if UNITY_EDITOR
        if (DeleteCustomTag)
        {
            // In case the Target GameObject has the custom Tag
            if (EditorTagManager.DoesGameObjectHasTag(Target, TagName))
            {
                // Target will be Untagged
                EditorTagManager.SetTagByIndex(Target, 0);
            }
            // Custom Tag gets deleted
            EditorTagManager.DeleteTag(TagName);
            DeleteCustomTag = false;
        }
#endif
    }
    #endregion
}