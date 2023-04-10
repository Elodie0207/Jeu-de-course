using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private bool isPaused = false;
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                isPaused = !isPaused;
    
                if (isPaused)
                {
                    Time.timeScale = 0; // arrêter le temps
                    // afficher le menu de pause ici
                }
                else
                {
                    Time.timeScale = 1; // reprendre le temps
                    // cacher le menu de pause ici
                }
            }
        }
}
