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
   
    
    //Lancement de la scène quand le mode de jeu est en solo
    public void SingleMode()
    {
       
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += LoadSceneComplete;

    }

    //lancement du jeu quand le jeu est en multijoueur
    public void MultiplayerMode()
    {
        Debug.Log("Multiplayer mode started");


        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += LoadSceneComplete;
        
        
    }

    //méthode qui va permettre de faire les modifications nécéssaires en fonction du mode de jeu comme le splitScreen ou la présence d'un deuxième joueur dans la scène
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

            GameObject[] multiplayerObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject multiplayerObject in multiplayerObjects)
            {
                if (multiplayerObject.name.StartsWith("M_"))
                {
                    multiplayerObject.SetActive(false);
                }
            }
            

            foreach (SplitCamera CameraSplit in SplitCamerasList)
            {
                CameraSplit.enabled = false;
            }
            
            MultiChoixVaisseau[] NoMulti = FindObjectsOfType<MultiChoixVaisseau>();

            foreach (MultiChoixVaisseau Choix in NoMulti)
            {
                Choix.enabled = false;
            }


        }


    }

}
