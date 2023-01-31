using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jouer(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       Debug.Log("hehe");
    }
    public void Quitter(){
        Application.Quit();
         Debug.Log("hehe");
    }
    // Update is called once per frame
    
}
