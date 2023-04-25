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

    public GameObject player2; 
    public GameMode CurrentMode = GameMode.Single;
    

    //l'état du jeu est en mode 1 joueur on charge juste la scène de base 
    public void SingleMode()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
        SplitCamera CameraSplit = FindObjectOfType<SplitCamera>();
        
        if (CameraSplit != null)
        {
            Debug.Log("SplitCamera found, enabling...");
            CameraSplit.enabled = true;
        }
        else
        {
            Debug.LogError("SplitCamera not found!");
        }

        Debug.Log("Multiplayer mode finished");

       


        GameObject instancePlayer2 = Instantiate(player2, new Vector3(-62.4f, 3f, 15f), Quaternion.identity);
    
        if (instancePlayer2 != null)
        {
            Debug.LogError("instantiate player2");
        }
        
         else
        {
            Debug.LogError("Could not instantiate player 2.");
        }
        
        
    
        Debug.Log("Multiplayer mode finished");
    }

}
