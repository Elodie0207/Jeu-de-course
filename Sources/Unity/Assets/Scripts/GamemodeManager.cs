using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//enum pour définir les différents mode de jeu
public enum GameMode
{
    Single,
    Multiplayer
}

public class GamemodeManager : MonoBehaviour
{
    
    //etat courant du jeu 
    private static GameMode CurrentMode = GameMode.Single;

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
        CurrentMode = GameMode.Multiplayer;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
        GameObject player1CameraFirstPerson = GameObject.Find("CamFirstPerson");
        player1CameraFirstPerson.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);
        
        GameObject player1CameraThirdPerson = GameObject.Find("CamThirdPerson");
        player1CameraThirdPerson.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);
        
        GameObject player1CamBack = GameObject.Find("CamBackPerson");
        player1CamBack.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);
        
        GameObject player2 = Resources.Load<GameObject>("PlayerCharacterRoot2");
        GameObject insancePlayer2 = Instantiate(player2, new Vector3(-64f, 2.9f, 41.3f), Quaternion.identity);
        
        



    }


}
