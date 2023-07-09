using UnityEngine;
using UnityEditor;

public class PrefabManager : EditorWindow
{
    GameObject prefab;
    GameObject course; // pour sélectionner la course
    bool isPlacingPrefab = false; // pour contrôler si on place des prefabs ou non

    [MenuItem("Tools/Prefab Placer")]
    static void OpenWindow()
    {
        PrefabManager window = (PrefabManager)GetWindow(typeof(PrefabManager));
        window.Show();
    }

    private void OnGUI()
    {
        course = (GameObject)EditorGUILayout.ObjectField("Course:", course, typeof(GameObject), true);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab:", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Start Placing Prefabs"))
        {
            if(prefab == null)
            {
                Debug.LogError("No prefab selected");
                return;
            }
            
            if(course == null)
            {
                Debug.LogError("No course selected");
                return;
            }
            
            isPlacingPrefab = true;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        if (GUILayout.Button("Stop Placing Prefabs"))
        {
            isPlacingPrefab = false;
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    private void OnSceneGUI(SceneView view)
    {
        if (!isPlacingPrefab) return;

        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject instance = Instantiate(prefab, hit.point, Quaternion.identity);
                instance.transform.SetParent(course.transform); // ajoute le prefab comme un enfant de la course
            }
        }
    }
}