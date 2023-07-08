using System;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public enum OPTIONS
{
    Banniere=1,
    Interstitiel=0
}

public class OpenExplorer : EditorWindow
{
    [FormerlySerializedAs("op")] public OPTIONS pubType;
    public OpenExplorerConfig config;
    public string filePath;

    private void CreateEditableField(string label, Action createAction)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        createAction();
        EditorGUILayout.EndHorizontal();
    }

    private void OnGUI()
    {
        if (config == null)
        {
            config = AssetDatabase.LoadAssetAtPath<OpenExplorerConfig>("Assets/Scripts/Editor/openExplorer.asset");
        }

        if (config == null)
        {
            config = CreateInstance<OpenExplorerConfig>();
        }

        CreateEditableField("drawer", () => config.importfileDrawer = EditorGUILayout.TextField(config.importfileDrawer));
        pubType = (OPTIONS)EditorGUILayout.EnumPopup("Type de la pub:", pubType);
        
        EditorGUILayout.LabelField("Selected File: ", filePath);
        
        if (GUILayout.Button("Select File"))
        {
            filePath = EditorUtility.OpenFilePanel("Select File", config.importfileDrawer, "");
        }

        if (GUILayout.Button("Importer File"))
        {
            string importedFilePath = Path.Combine(config.destinationDrawer, Path.GetFileName(filePath));
            FileUtil.CopyFileOrDirectory(filePath, importedFilePath);

            AssetDatabase.ImportAsset(importedFilePath, ImportAssetOptions.Default);
            
            Object importedAsset = AssetDatabase.LoadAssetAtPath(importedFilePath, typeof(Texture2D));
            if (importedAsset != null)
            {
                TextureImporter textureImporter = AssetImporter.GetAtPath(importedFilePath) as TextureImporter;
                if (textureImporter != null)
                {
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.spriteImportMode = SpriteImportMode.Single;
                    textureImporter.SaveAndReimport();
                }
            }
            
        }

        if (GUILayout.Button("Save Config"))
        {
            config.importfileDrawer = Path.GetDirectoryName(filePath);
            EditorUtility.SetDirty(config);
            if (AssetDatabase.LoadAssetAtPath<OpenExplorer>("Assets/Scripts/Editor/openExplorer.asset") == null)
            {
                AssetDatabase.CreateAsset(config, "Assets/Scripts/Editor/openExplorer.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Tools/OpenExplorer")]
    static void OpenExplorerWindow()
    {
        var window = GetWindow<OpenExplorer>();
        window.Show();
    }
}