using System;
using UnityEngine;

[Serializable]
public class OpenExplorerConfig : ScriptableObject
{
    public string importfileDrawer;
    public string destinationDrawer = "Assets/Resources/PubImage";
    public int theme;
    private void OnEnable()
    {
        importfileDrawer = Application.dataPath;
    }
}
