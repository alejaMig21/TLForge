using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to implement a way to import data from an asset
/// </summary>
public interface IDataImporter
{
    #region PROPERTIES
    public string ImportationPath { get; set; }
    public bool StartImport {  get; set; }
    #endregion

    #region METHODS
    public void Import(string path);
    public void ApplyData();
    #endregion
}