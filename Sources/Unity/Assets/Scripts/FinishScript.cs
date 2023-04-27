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

    public int nbTours = 0;
    public int score = 0;
    public int nbcheckpointsPasser = 0;
	public GameObject canvas;
   	//private Image image;
    public GameObject[] checkpoints; // Tableau de tous les checkpoints à passer
    private bool[] checkpointsPasser; // Tableau pour suivre les checkpoints franchis
  
   	public float waitTime = 5f;
	public  bool cheat=false;
	public bool fin=false; 


    void Start()
    {
        // Initialiser le tableau des checkpoints franchis
        checkpointsPasser = new bool[checkpoints.Length];
		 //image =canvas.GetComponentInChildren<Image>();
    	
    // Masquer l'image
    	//image.gameObject.SetActive(false);
        ScoreBoard.enabled = false;

    }

  

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && nbcheckpointsPasser == checkpoints.Length || other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && cheat==true)
        {
            Debug.Log("Finish");
            
            nbTours++;

            if (nbTours == 3)
            {
				Debug.Log("ah");
        	// Activer l'image
        	//image.gameObject.SetActive(true);
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
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}

}