using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameMode
{
    Single,
    Multiplayer
}

public class GamemodeManager : MonoBehaviour
{
    
    public GameMode CurrentMode;
   
    
    //l'état du jeu est en mode 1 joueur on charge juste la scène de base 
    public void SingleMode()
    {
       
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += LoadSceneComplete;

    }

    //l'état du jeu est en multijoueur dans ce cas la après le chargement de la scène on va:
    //modifier la largeur des différentes caméra du premier joueur pour avoir un écran splitté
    //instancier la prefab du deuxième joueur dans la scène pour avoir le deuxième joueur 
    public void MultiplayerMode()
    {
        Debug.Log("Multiplayer mode started");


        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += LoadSceneComplete;
        
        
    }

    private void LoadSceneComplete(AsyncOperation asyncOperation)
    {
        SplitCamera[] SplitCamerasList = FindObjectsOfType<SplitCamera>();

        if (CurrentMode == GameMode.Multiplayer)
        {
            foreach (SplitCamera CameraSplit in SplitCamerasList)
            {
                CameraSplit.enabled = true;
            }
        }

        else if (CurrentMode == GameMode.Single)
        {

            GameObject MultiPrefab = GameObject.Find("MULTI");


            if (MultiPrefab != null)
            {
                MultiPrefab.SetActive(false); // Désactiver l'objet "Multi"
            }

            foreach (SplitCamera CameraSplit in SplitCamerasList)
            {
                CameraSplit.enabled = false;
            }


        }



        /*GameObject instancePlayer2 = Instantiate(player2, new Vector3(-62.4f, 3f, 15f), Quaternion.identity);

        if (instancePlayer2 != null)
        {
            Debug.LogError("instantiate player2");
        }

        else
        {
            Debug.LogError("Could not instantiate player 2.");
        }



        Debug.Log("Multiplayer mode finished");
    }*/




    }

}
