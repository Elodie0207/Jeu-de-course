using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{	
public GameObject boutton_jouer;
public GameObject boutton_quitter;
private string langage="";
public GameObject boutton; 
	
    // Start is called before the first frame update
    public void Jouer(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }
    public void Quitter(){
        Application.Quit();
         
    }
	
	public void Langage(){
		
		if(langage==""){
			langage="fr";
		}

		if(langage=="fr"){
           	boutton_jouer.GetComponent<Text>().text = "Jouer";
            boutton_quitter.GetComponent<Text>().text = "Quitter";
            langage = "en";
        }
        else if(langage=="en"){
            boutton_jouer.GetComponent<Text>().text = "Play";
           boutton_quitter.GetComponent<Text>().text = "Quit";
            langage="fr";
        }
			

}
	}


