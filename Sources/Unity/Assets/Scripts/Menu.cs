using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jouer(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }
    public void Quitter(){
        Application.Quit();
         
    }
    // Update is called once per frame
    
}
