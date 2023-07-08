using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Networking;
using Object = UnityEngine.Object;
using System;
using Unity.EditorCoroutines.Editor;

//Type de pub
public enum OPTIONS
{
    Banniere=1,
    Interstitiel=0
}

public class OpenExplorer : EditorWindow
{
    public OPTIONS pubType;
    public OpenExplorerConfig config;
    public string filePath;
    private bool isFileSelect = false;

    private void CreateEditableField(string label, Action createAction)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label);
        createAction();
        EditorGUILayout.EndHorizontal();
    }

    private void OnGUI()
    {
        //load config si elle existe
        if (config == null)
        {
            config = AssetDatabase.LoadAssetAtPath<OpenExplorerConfig>("Assets/Scripts/Editor/openExplorer.asset");
        }

        if (config == null)
        {
            config = CreateInstance<OpenExplorerConfig>();
        }

        //folder ou se trouve l'image a import
        CreateEditableField("drawer", () => config.importfileDrawer = EditorGUILayout.TextField(config.importfileDrawer));
        
        //theme pub
        CreateEditableField("theme", () => config.theme = EditorGUILayout.IntSlider(config.theme, 1, 6));
        pubType = (OPTIONS)EditorGUILayout.EnumPopup("Type de la pub:", pubType);

        EditorGUILayout.LabelField("Selected File: ", filePath);
        
        if (GUILayout.Button("Select File"))
        {
            filePath = EditorUtility.OpenFilePanel("Select File", config.importfileDrawer, "");
            isFileSelect = true;
        }

        //disable btn tant qu'aucun fichier n'est select
        GUI.enabled = isFileSelect;
        if (GUILayout.Button("Importer File"))
        {
            string importedFilePath = Path.Combine(config.destinationDrawer, Path.GetFileName(filePath));
            FileUtil.CopyFileOrDirectory(filePath, importedFilePath);

            //import le fichier dans le dossier "Assets/Resources/PubImage" 
            AssetDatabase.ImportAsset(importedFilePath, ImportAssetOptions.Default);
            
            //change le type de fichier en Sprite 2D
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
            EditorCoroutineUtility.StartCoroutineOwnerless(CreatePub(Path.GetFileNameWithoutExtension(importedFilePath)));
        }
        
        GUI.enabled = true;

        if (GUILayout.Button("Save Config"))
        {
            //save dossier d'import
            config.importfileDrawer = Path.GetDirectoryName(filePath);
            EditorUtility.SetDirty(config);
            if (AssetDatabase.LoadAssetAtPath<OpenExplorerConfig>("Assets/Scripts/Editor/openExplorer.asset") == null)
            {
                AssetDatabase.CreateAsset(config, "Assets/Scripts/Editor/openExplorer.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Tools/ImportPub")]
    static void OpenExplorerWindow()
    {
        var window = GetWindow<OpenExplorer>();
        window.Show();
    }
    
    //send requete pour creer une pub dans la bdd
    public IEnumerator CreatePub(string fileName)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //cree json body avec val
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.Append("\"theme\": \"" + config.theme + "\",");
            jsonBuilder.Append("\"lien\": \"" + fileName + "\",");
            jsonBuilder.Append("\"isBanniere\": \"" + (int)pubType + "\"");
            jsonBuilder.Append("}");
            
            
            byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

            //cree requete
            UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/pub", UnityWebRequest.kHttpVerbPOST);
            webRequest.uploadHandler = new UploadHandlerRaw(postData);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            //envoie requete
            yield return webRequest.SendWebRequest();
            
            //gestion erreur
            if (webRequest.result == UnityWebRequest.Result.ConnectionError )
            {
                webRequest.Dispose();
            }
            //libere ressources
            webRequest.Dispose();
        }
    }
}
