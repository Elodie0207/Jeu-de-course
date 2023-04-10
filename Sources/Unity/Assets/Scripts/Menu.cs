using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{ 
	public Button buttonResol;
public GameObject boutton_jouer;
public GameObject boutton_quitter;
private string langage="";
public GameObject boutton;
public int test = 0;
public void Start()
{
	Screen.fullScreen = true; 
	buttonResol.gameObject.SetActive(false); 
}
    // Start is called before the first frame update
    public void Jouer(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }
    public void Quitter(){
        Application.Quit();
         
    }
    public void Param(){
	    buttonResol.gameObject.SetActive(true); 
         
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

	public void fullScreen()
	{
		if (Screen.fullScreen)
		{
			Screen.fullScreen = false;

			Screen.SetResolution(1280, 720, false);
			buttonResol.gameObject.SetActive(false); 
		}
		else
		{
			Screen.fullScreen = true; 
			buttonResol.gameObject.SetActive(false); 
		}
	}
	}


