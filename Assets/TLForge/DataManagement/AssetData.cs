using UnityEngine;

/// <summary>
/// ScriptableObject that stores importation and exportation addresses.
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObject/Asset Data", order = 1)]
public class AssetData : ScriptableObject
{
    public string assetName;
    public string importationPath;
    public string exportationPath;
}