using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
 public GameObject[] maps; // Tableau contenant les différents GameObjects représentant les cartes
    public string[] listeMaps = new string[6]; // Tableau contenant les noms des cartes dans l'ordre où elles sont chargées
    private int currentMapIndex = 0;

void Start(){

 string mapName = PlayerPrefs.GetString("map");
        Debug.Log(mapName+"jjjj");
		
		LoadMap(mapName);


}


 private void LoadMap(string mapName) {
        // Désactivation de toutes les cartes
        foreach (GameObject map in maps) {
            map.SetActive(false);
        }

        // Recherche de la carte à charger
        GameObject mapToLoad = null;
        foreach (GameObject map in maps) {
            if (map.name == mapName) {
                mapToLoad = map;
                break;
            }
        }

        // Si la carte n'a pas été trouvée, on charge la première carte du tableau de cartes
        if (mapToLoad == null) {
            mapToLoad = maps[0];
        }

        // Activation de la carte à charger
        mapToLoad.SetActive(true);
        
        

        // Ajout du nom de la carte à la liste des cartes chargées
        listeMaps[currentMapIndex] = mapToLoad.name;
        currentMapIndex++;

        // Si toutes les cartes ont été chargées, on remet l'indice courant à 0 pour recommencer au début de la liste
        if (currentMapIndex >= listeMaps.Length) {
            currentMapIndex = 0;
        }
this.enabled = false;
    }
   
}
