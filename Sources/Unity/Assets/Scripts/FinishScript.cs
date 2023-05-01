using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class FinishScript : MonoBehaviour
{
    public GameObject tour;
    public Rigidbody rbVehicule; 
    public Canvas ScoreBoard;

    public GameObject Maps; 
    public int nbTours = 0;
    public int score = 0;
    public int nbcheckpointsPasser = 0;
	public GameObject canvas;
    public RaceManager RaceManager;
    public GameObject[] checkpoints; // Tableau de tous les checkpoints à passer
    private bool[] checkpointsPasser; // Tableau pour suivre les checkpoints franchis
    public float waitTime = 10f;
	public  bool cheat=false;
	public bool fin=false;
    private string map;
    GameObject piste1 ;
    GameObject piste2 ;
    GameObject piste3 ;
    GameObject piste4 ;
   
    void Start()
    {
         map = PlayerPrefs.GetString("map");
        checkpointsPasser = new bool[checkpoints.Length];
        ScoreBoard.enabled = false;
        Maps.GetComponent<ChoixMap>();
        piste1 = Maps.GetComponent<ChoixMap>().Piste1;
        piste2 = Maps.GetComponent<ChoixMap>().Piste2;
       piste3 = Maps.GetComponent<ChoixMap>().Piste3;
       piste4 = Maps.GetComponent<ChoixMap>().Piste4;
    }

  

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && nbcheckpointsPasser == checkpoints.Length || other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && cheat==true)
        {
            Debug.Log("Finish");
            
            nbTours++;

            if (nbTours == 3)
            {
                
                fin = true;
                Debug.Log(fin);
                
            RaceManager.UpdateScore();
            ScoreBoard.enabled=true;

       	 	// Démarrer une coroutine pour attendre 5 secondes
        	StartCoroutine(WaitForNextScene());
            
            }
            else
            {
                tour.GetComponent<Text>().text = nbTours.ToString();
            }

            // Réinitialiser le tableau des checkpoints franchis
            for (int i = 0; i < checkpointsPasser.Length; i++)
            {
                checkpointsPasser[i] = false;
            }
            nbcheckpointsPasser = 0;
        }
        else if (other.tag == "Checkpoint")
        {
 			
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (other.gameObject == checkpoints[i] && checkpointsPasser[i] == false)
                {
                    Debug.Log("Checkpoint " + (i+1));
                    checkpointsPasser[i] = true;
                    nbcheckpointsPasser++;
                    break;
                    
                }
            }
        }
    }
	
  void Update(){
 cheat = FindObjectOfType<Autres>().cheatcode;
}
private IEnumerator WaitForNextScene()
{
    // Attendre 5 secondes
    yield return new WaitForSeconds(waitTime);

    // Passer à la ligne suivante
    ScoreBoard.enabled=false;
    
    if (map == "Map1")
    {
        map = "Map2";
        piste1.SetActive(false);
        piste2.SetActive(true);
        piste3.SetActive(false);
        piste4.SetActive(false);
    }
    else if (map == "Map2")
    {
        map = "Map3";
        piste1.SetActive(false);
        piste2.SetActive(false);
        piste3.SetActive(true);
        piste4.SetActive(false);
    }
    else if (map == "Map3")
    {
        map = "Map4";
        piste1.SetActive(false);
        piste2.SetActive(false);
        piste3.SetActive(false);
        piste4.SetActive(true);
    }
    else if (map == "Map4")
    {
        map = "fin";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
   
    
}


}