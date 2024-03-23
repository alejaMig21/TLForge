using UnityEngine;
using System.IO;

#if UNITY_EDITOR
public class TLFLayerCollisionMatrix2D : MonoBehaviour, IDataExporter
{
    #region FIELDS
    [SerializeField]
    private bool[,] collisionMatrix = new bool[32, 32];
    [SerializeField]
    private string assetName = "LayerCollisionMatrix";
    [SerializeField]
    private string importationPath = "Assets/EditorData/CollisionData/";
    [SerializeField]
    private bool startImport = false;
    [SerializeField]
    private string exportationPath = "Assets/EditorData/CollisionData/";
    [SerializeField]
    private bool startExport = false;
    #endregion

    #region PROPERTIES
    public bool[,] CollisionMatrix { get => collisionMatrix; set => collisionMatrix = value; }
    public string AssetName { get => assetName; set => assetName = value; }
    public string ImportationPath { get => importationPath; set => importationPath = value; }
    public bool StartImport { get => startImport; set => startImport = value; }
    public string ExportationPath { get => exportationPath; set => exportationPath = value; }
    public bool StartExport { get => startExport; set => startExport = value; }
    public ScriptableObject Data { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    #endregion

    #region METHODS
    public bool GetCollision(int layer1, int layer2)
    {
        return CollisionMatrix[layer1, layer2];
    }
    public void SetCollision(int layer1, int layer2, bool value)
    {
        CollisionMatrix[layer1, layer2] = value;
        CollisionMatrix[layer2, layer1] = value; // Update both sides of the matrix since it's symmetrical
    }
    public void ApplyCollisionsForLayer(int layer)
    {
        for (int otherLayer = 0; otherLayer < 32; otherLayer++)
        {
            IgnoreLayerCollision(layer, otherLayer, !CollisionMatrix[layer, otherLayer]);
            IgnoreLayerCollision(otherLayer, layer, !CollisionMatrix[layer, otherLayer]); // Update both sides of the Matrix
        }
    }
    public void SetAllCollisions(bool value)
    {
        for (int i = 0; i < collisionMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < collisionMatrix.GetLength(1); j++)
            {
                SetCollision(i, j, value);
            }
        }
    }
    /// <summary>
    /// Method to save collision matrix to a JSON file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="assetName"></param>
    public void Export(string path, string assetName)
    {
        DirectoryManager.CreatePath(path);

        CollisionMatrixData data = new(collisionMatrix);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path + assetName + ".json", json);

        Debug.Log("CollisionMatrix exported to: " + ExportationPath + AssetName);
    }
    /// <summary>
    /// Method to load collision matrix from a JSON file
    /// </summary>
    /// <param name="path"></param>
    public void Import(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("There is no CollisionMatrix to import at " + path);
            return;
        }
        string json = File.ReadAllText(path);
        CollisionMatrixData data = JsonUtility.FromJson<CollisionMatrixData>(json);

        int rows = data.matrixData.Length;
        int cols = data.matrixData[0].array.Length;

        collisionMatrix = new bool[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                collisionMatrix[i, j] = data.matrixData[i].array[j];
            }
        }
        // Debug.Log("CollisionMatrix imported from: " + ImportationPath + AssetName);
    }
    /// <summary>
    /// Method to load collision matrix from a JSON file
    /// </summary>
    /// <param name="path"></param>
    public void Import(string path, bool addExtension)
    {
        string extension = !addExtension ? "" : ".json";

        Import(path + extension);
    }
    /// <summary>
    /// Method to apply the collisions stored in the local matrix to the global Unity's Collision Matrix in Physics
    /// </summary>
    public void ApplyData()
    {
        for (int layer1 = 0; layer1 < 32; layer1++)
        {
            for (int layer2 = 0; layer2 < 32; layer2++)
            {
                IgnoreLayerCollision(layer1, layer2, !CollisionMatrix[layer1, layer2]);
            }
        }
    }
    public virtual void IgnoreLayerCollision(int layer1, int layer2, bool collisionValue)
    {
        Physics2D.IgnoreLayerCollision(layer1, layer2, collisionValue);
    }
    #endregion
}
#endif