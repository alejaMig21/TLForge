using System;

[Serializable]
public class CollisionMatrixData
{
    #region FIELDS
    public SerializableBoolArray[] matrixData;
    #endregion

    #region METHODS
    public CollisionMatrixData(bool[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        matrixData = new SerializableBoolArray[rows];
        for (int i = 0; i < rows; i++)
        {
            matrixData[i] = new SerializableBoolArray { array = new bool[cols] };
            for (int j = 0; j < cols; j++)
            {
                matrixData[i].array[j] = matrix[i, j];
            }
        }
    }
    #endregion
}