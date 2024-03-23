using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(CustomProjectSetting), true)]
public class CustomProjectSettingEditor : CustomInformationEditor
{
    #region FIELDS
    private FieldInfo[] boolFields;
    #endregion

    #region PROPERTIES
    public FieldInfo[] BoolFields { get => boolFields; set => boolFields = value; }
    #endregion

    #region METHODS
    private void OnEnable()
    {
        SetUpValues();
    }

    public override void OnInspectorGUI()
    {
        DrawCustomPropertiesFramework();
    }

    protected override void SetUpValues()
    {
        base.SetUpValues();

        // Get all bool fields of the current class
        Type targetType = target.GetType();
        BoolFields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                            .Where(f => f.FieldType == typeof(bool))
                                            .ToArray();
    }
    protected void DrawCustomPropertiesFramework()
    {
        // Draw the default Inspector for the base class
        DrawDefaultInspector();

        GUILayout.Space(5f);

        // Show buttons for bool fields
        foreach (FieldInfo field in BoolFields)
        {
            if (GUILayout.Button(TransformButtonText(field.Name)))
            {
                field.SetValue(target, true);

                // Execute the CheckStatus method
                MethodInfo checkStatusMethod = target.GetType().GetMethod("CheckStatus", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                checkStatusMethod?.Invoke(target, null);
            }

            GUILayout.Space(5f);
        }
    }

    public override bool HasPreviewGUI()
    {
        // Only show the default Inspector if there are hidden fields
        foreach (FieldInfo field in BoolFields)
        {
            HideInInspector hideInInspector = field.GetCustomAttribute<HideInInspector>();
            if (hideInInspector != null)
            {
                return false;
            }
        }
        return base.HasPreviewGUI();
    }
    #endregion
}