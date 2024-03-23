using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to implement a way to export data to an asset
/// </summary>
public interface IDataExporter : IDataImporter
{
    #region PROPERTIES
    public string AssetName { get; set; }
    public string ExportationPath { get; set; }
    public bool StartExport {  get; set; }
    #endregion

    #region METHODS
    public void Export(string path, string assetName);
    #endregion
}