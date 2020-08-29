using UnityEngine.UI;

internal static class Tools
{
    internal static void SetText(this Text textObject, string text)
    {
        if (textObject != null)
        {
            textObject.text = text;
        }
    }
}
