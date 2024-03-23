using UnityEditor;

public class CustomInformationEditor : Editor
{
    #region METHODS
    protected virtual void SetUpValues()
    {
        // Do something
    }
    /// <summary>
    /// Method to modify button text
    /// </summary>
    /// <param name="originalText">Button text</param>
    /// <returns></returns>
    protected string TransformButtonText(string originalText)
    {
        // Replace camel case with spaces
        string resultText = System.Text.RegularExpressions.Regex.Replace(originalText, "(\\B[A-Z])", " $1");

        // Convert to uppercase
        string upperCaseText = resultText.ToUpper();

        // Replace low bars with &
        upperCaseText = upperCaseText.Replace("_", " &");

        return upperCaseText;
    }
    #endregion
}