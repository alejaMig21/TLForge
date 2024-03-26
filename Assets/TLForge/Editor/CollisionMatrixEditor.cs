using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TLFCollisionMatrix2D), true)]
public class CollisionMatrixEditor : CustomInformationEditor
{
    #region FIELDS
    [SerializeField]
    private TLFCollisionMatrix2D collisionMatrixScript;
    [SerializeField]
    private bool[,] collisionMatrix = new bool[32, 32];
    private const short SPACE_BETWEEN_BUTTONS = 5;
    private const short SPACE_BETWEEN_AREAS = 20;
    #endregion

    #region PROPERTIES
    public TLFCollisionMatrix2D CollisionMatrixScript { get => collisionMatrixScript; set => collisionMatrixScript = value; }
    public bool[,] CollisionMatrix { get => collisionMatrix; set => collisionMatrix = value; }
    #endregion

    #region METHODS
    private void OnEnable()
    {
        SetUpValues();
    }
    public override void OnInspectorGUI()
    {
        DrawMatrixFramework();

        CollisionMatrixScript.CollisionMatrix = CollisionMatrix;

        if (GUI.changed)
        {
            EditorUtility.SetDirty(CollisionMatrixScript);
            AssetDatabase.SaveAssets();
        }
    }
    protected override void SetUpValues()
    {
        base.SetUpValues();

        CollisionMatrixScript = target as TLFCollisionMatrix2D;
        
        if(CollisionMatrixScript == null)
        {
            return;
        }

        LoadCollisionMatrixWithExtension(true);
    }
    protected void DrawMatrixFramework()
    {
        DrawPaths();

        DrawLegend();

        DrawMatrix();

        GUILayout.Space(SPACE_BETWEEN_AREAS);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(TransformButtonText("Enable All Collisions")))
        {
            EnableAllCollisions();
        }

        GUILayout.Space(SPACE_BETWEEN_BUTTONS);

        if (GUILayout.Button(TransformButtonText("Disable All Collisions")))
        {
            DisableAllCollisions();
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(SPACE_BETWEEN_BUTTONS);

        // Draw buttons for exporting and importing the matrix
        if (GUILayout.Button(TransformButtonText("ExportMatrix")))
        {
            ApplyCollisions();
        }

        GUILayout.Space(SPACE_BETWEEN_BUTTONS);

        if (GUILayout.Button(TransformButtonText("ImportMatrix")))
        {
            LoadCollisionMatrixWithExtension(true);
        }

        GUILayout.Space(SPACE_BETWEEN_BUTTONS);

        if (GUILayout.Button(TransformButtonText("ImportExistingPreset")))
        {
            // Open File Panel based on CollisionMatrixScript.Root
            string path = EditorUtility.OpenFilePanel("Select Existing Preset", CollisionMatrixScript.Root, "json");

            if (path == string.Empty)
            {
                return;
            }

            // Check if the selected path is valid
            if (path.Contains(CollisionMatrixScript.Root))
            {
                // Convert absolute file path to relative path within Assets
                string relativePath = "Assets" + path[Application.dataPath.Length..];

                CollisionMatrixScript.ExportationPath = relativePath[..(relativePath.LastIndexOf("/") + 1)];
                CollisionMatrixScript.ImportationPath = CollisionMatrixScript.ExportationPath;
                CollisionMatrixScript.AssetName = Path.GetFileName(relativePath);

                LoadCollisionMatrixWithExtension(false);

                // Update the asset name by removing the file extension
                CollisionMatrixScript.AssetName = Path.GetFileNameWithoutExtension(CollisionMatrixScript.AssetName);
            }
            else
            {
                // Show error if selection is invalid or not within Resources folder
                Debug.LogError("The selected file must be within the " + CollisionMatrixScript.Root + "folder.");
            }
        }
    }
    private void DrawLegend()
    {
        EditorGUILayout.LabelField("Layers:", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal(); // Start a new row for indexes
        GUILayout.Space(110); // Space for the first Label that says "Layers"

        // Draw the layer indices above the checkboxes
        for (int j = 0; j < CollisionMatrix.GetLength(1); j++)
        {
            if (!string.IsNullOrEmpty(LayerMask.LayerToName(j)))
            {
                GUILayout.Label(j.ToString(), EditorStyles.centeredGreyMiniLabel, GUILayout.Width(20)); // Draw the layer index
            }
        }
        EditorGUILayout.EndHorizontal(); // Ends row of layer indexes
    }
    private void DrawMatrix()
    {
        // Draw the layers and checkboxes below the indexes
        for (int i = 0; i < CollisionMatrix.GetLength(0); i++)
        {
            if (!string.IsNullOrEmpty(LayerMask.LayerToName(i)))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i + ": " + LayerMask.LayerToName(i), GUILayout.Width(110));
                DrawCollisionCheckboxes(i);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
    public void DrawPaths()
    {
        // Add fields to edit the import and export path
        EditorGUILayout.LabelField("Paths:", EditorStyles.boldLabel);
        CollisionMatrixScript.AssetName = EditorGUILayout.TextField("Asset Name", CollisionMatrixScript.AssetName);
        if (string.IsNullOrEmpty(CollisionMatrixScript.AssetName))
        {
            CollisionMatrixScript.AssetName = "DefaultAssetName"; // Make sure you have a default value
        }

        CollisionMatrixScript.ExportationPath = EditorGUILayout.TextField("Exportation Path", CollisionMatrixScript.ExportationPath);
        if (string.IsNullOrEmpty(CollisionMatrixScript.ExportationPath))
        {
            CollisionMatrixScript.ExportationPath = CollisionMatrixScript.Root; // Make sure you have a default value
        }

        CollisionMatrixScript.ImportationPath = EditorGUILayout.TextField("Importation Path", CollisionMatrixScript.ImportationPath);
        if (string.IsNullOrEmpty(CollisionMatrixScript.ImportationPath))
        {
            CollisionMatrixScript.ImportationPath = CollisionMatrixScript.Root; // Make sure you have a default value
        }

        // Add buttons to select the import and export path
        GUILayout.Space(20);

        CollisionMatrixScript.ExportationPath = SelectPath("Select Export Path", "Select Export Folder", CollisionMatrixScript.ExportationPath);

        GUILayout.Space(5);

        CollisionMatrixScript.ImportationPath = SelectPath("Select Import Path", "Select Import Folder", CollisionMatrixScript.ImportationPath);

        GUILayout.Space(20);
    }
    /// <summary>
    /// Method to select a folder path and update the corresponding properties
    /// </summary>
    /// <param name="buttonName"></param>
    /// <param name="windowName"></param>
    /// <param name="Path"></param>
    /// <returns></returns>
    private string SelectPath(string buttonName, string windowName, string defaultPath)
    {
        if (GUILayout.Button(TransformButtonText(buttonName)))
        {
            // Ensure initial path is within Resources folder
            string resourcesPath = Application.dataPath + "/Resources";
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

            string path = EditorUtility.SaveFolderPanel(windowName, resourcesPath, "");

            // Verifies if path is within Resources folder
            if (path.StartsWith(resourcesPath))
            {
                // Converts the absolute path of the file to a path relative to Assets/Resources
                path = "Assets" + path[Application.dataPath.Length..] + "/";
                defaultPath = path;
            }
            else
            {
                // Handles the case where the selected path is not within Resources
                Debug.LogError("Selected path must be within Resources folder");
            }
        }
        return defaultPath;
    }
    private void DrawCollisionCheckboxes(int layerIndex)
    {
        // Draw checkboxes for collisions with other layers
        for (int j = 0; j < CollisionMatrix.GetLength(1); j++)
        {
            if (!string.IsNullOrEmpty(LayerMask.LayerToName(j)))
            {
                bool newValue = EditorGUILayout.Toggle(CollisionMatrix[layerIndex, j], GUILayout.Width(20));
                if (newValue != CollisionMatrix[layerIndex, j])
                {
                    // Update the collision value
                    CollisionMatrix[layerIndex, j] = newValue;
                    // Reflect the change symmetrically
                    CollisionMatrix[j, layerIndex] = newValue;
                }
            }
        }
    }
    private void LoadCollisionMatrixWithExtension(bool addExtension)
    {
        CollisionMatrixScript.ImportWithExtension(CollisionMatrixScript.ImportationPath + CollisionMatrixScript.AssetName, addExtension);
        CollisionMatrix = CollisionMatrixScript.CollisionMatrix;
    }
    private void ApplyCollisions()
    {
        CollisionMatrixScript.ApplyData();
        CollisionMatrixScript.Export(CollisionMatrixScript.ExportationPath, CollisionMatrixScript.AssetName);
    }
    private void EnableAllCollisions()
    {
        CollisionMatrixScript.SetAllCollisions(true);
    }
    private void DisableAllCollisions()
    {
        CollisionMatrixScript.SetAllCollisions(false);
    }
    #endregion
}