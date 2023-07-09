using UnityEngine;
using UnityEditor;

public class PrefabManager : EditorWindow
{
    GameObject prefab;
    GameObject course; // pour sélectionner la course

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

        if (GUILayout.Button("Place Prefab"))
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

            SceneView.duringSceneGui += view =>
            {
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
            };
        }
    }
}