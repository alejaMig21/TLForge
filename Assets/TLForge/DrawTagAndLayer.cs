using UnityEngine;

public class DrawTagAndLayer : MonoBehaviour
{
    #region METHODS
    private void OnGUI()
    {
        GUIStyle style = new();
        style.fontSize = 60;
        style.normal.textColor = Color.white;

        string text = "Tag: " + gameObject.tag + "\nLayer: " + LayerMask.LayerToName(gameObject.layer);
        GUI.Label(new Rect(10, 10, 800, 200), text, style);
    }
    #endregion
}