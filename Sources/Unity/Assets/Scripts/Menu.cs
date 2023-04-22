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
    //public GameObject boutton;
    public int test = 0;
    public GameObject CanvaCourrant;
    public GameObject nouveauCanva;
	public GameObject CanvaMap;
    public GameObject CanvaVaisseau;
	 public GameObject CanvaJoueur;
    public Dropdown drop;
    public Button boutton_retour;
    public GamemodeManager ManagerMode;
	private string map="";
public void Start()
{

	    CanvaCourrant.SetActive(true); 
	    nouveauCanva.SetActive(false);
		CanvaMap.SetActive(false);
		CanvaVaisseau.SetActive(false);
		CanvaJoueur.SetActive(false);
	    Screen.fullScreen = true; 
	   
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
Debug.Log("Le bouton Jouer a été cliqué");
CanvaJoueur.SetActive(true);
   
		CanvaCourrant.SetActive(false); 
		
    }

    public void Quitter(){
        Application.Quit();
    }
    public void Param(){
	   
        CanvaCourrant.SetActive(false); 
		nouveauCanva.SetActive(true); 
    }
	public void Solo(){
CanvaMap.SetActive(true);
CanvaJoueur.SetActive(false);

//ManagerMode.SingleMode();
}
	public void Multi(){
CanvaMap.SetActive(true);
CanvaJoueur.SetActive(false);
ManagerMode.CurrentMode = GameMode.Multiplayer;
//ManagerMode.MultiplayerMode();
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
public void OnImageClick(UnityEngine.UI.Button button){
string objectName = button.gameObject.name;
  map = objectName;
CanvaMap.SetActive(false);
		CanvaVaisseau.SetActive(true);
Debug.Log(map);
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


