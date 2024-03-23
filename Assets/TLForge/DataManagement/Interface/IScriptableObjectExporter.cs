using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Interface to implement a way to export and import data from a ScriptableObject
/// </summary>
public interface IScriptableObjectExporter : IDataExporter
{
    #region PROPERTIES
    public ScriptableObject Data { get; set; }
    #endregion

    #region METHODS
    public void ApplyData(ScriptableObject data);
    #endregion
}