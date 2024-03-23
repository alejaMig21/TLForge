using UnityEngine;

/// <summary>
/// Static class that handles Layers features during Runtime mode
/// </summary>
public static class RuntimeLayersManager
{
    #region METHODS

    public static bool LayerExists(string layerName)
    {
        return GetLayerIndex(layerName) != -1;
    }

    public static int GetLayerIndex(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

    public static LayerMask LayerNameToMask(string layerName)
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex == -1)
        {
            Debug.LogWarning("Layer not found: " + layerName);
            return 0;
        }
        return 1 << layerIndex;
    }

    public static LayerMask CombineLayersToMask(string[] layerNames)
    {
        LayerMask combinedMask = 0;
        foreach (var layerName in layerNames)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);
            if (layerIndex != -1)
            {
                combinedMask |= 1 << layerIndex;
            }
            else
            {
                Debug.LogWarning("Layer not found: " + layerName);
            }
        }
        return combinedMask;
    }
    #endregion
}