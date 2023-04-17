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
    //public GameObject boutton;
    public int test = 0;
    public GameObject CanvaCourrant;
    public GameObject nouveauCanva;
	public GameObject CanvaMap;
    public GameObject CanvaVaisseau;
    public Dropdown drop;
    public Button boutton_retour;
public void Start()
{
	    CanvaCourrant.SetActive(true); 
	    nouveauCanva.SetActive(false);
		CanvaMap.SetActive(false);
		CanvaVaisseau.SetActive(false);
	    Screen.fullScreen = true; 
	    buttonResol.gameObject.SetActive(false); 
        drop.options.Clear();
        drop.options.Add(new Dropdown.OptionData("Fullscreen"));
        drop.options.Add(new Dropdown.OptionData("Fenêtre"));
        drop.options.Add(new Dropdown.OptionData("1920x1080"));
        drop.options.Add(new Dropdown.OptionData("2560x1440"));
		drop.onValueChanged.AddListener(delegate
        {
            OnScreenModeChanged(drop.value);
        });
}
    // Start is called before the first frame update
    public void Jouer(){
		CanvaMap.SetActive(true);
		CanvaCourrant.SetActive(false); 
	    nouveauCanva.SetActive(false);
		CanvaMap.SetActive(false);
		CanvaVaisseau.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Quitter(){
        Application.Quit();
    }
    public void Param(){
	    buttonResol.gameObject.SetActive(true); 
        CanvaCourrant.SetActive(false); 
		nouveauCanva.SetActive(true); 
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
public void OnScreenModeChanged(int value)
    {
        switch (value)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.fullScreen = false;
                break;
            case 2:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            default:
                Debug.LogError("Erreur");
                break;
        }
    }
        public void Retour(){
 	        CanvaCourrant.SetActive(true); 
		    nouveauCanva.SetActive(false); 
        }
	}


