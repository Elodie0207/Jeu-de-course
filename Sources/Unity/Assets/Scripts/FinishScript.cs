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
    private int nbTours = 0;
    private bool estEnCourse = false;
    public GameObject[] checkpoints; // Tableau de tous les checkpoints à passer
    private bool[] checkpointsPasser; // Tableau pour suivre les checkpoints franchis
    private int nbcheckpointsPasser = 0;
   /* public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public RawImage rawImage2;
    public VideoPlayer videoPlayer2;*/
    void Start()
    {
        // Initialiser le tableau des checkpoints franchis
        checkpointsPasser = new bool[checkpoints.Length];
        /*videoPlayer.enabled = false;
        videoPlayer2.enabled = false;*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && nbcheckpointsPasser == checkpoints.Length && estEnCourse == false)
        {
            Debug.Log("Finish");
            estEnCourse = true;
            nbTours++;

            if (nbTours == 3)
            {
                /*videoPlayer = gameObject.GetComponent<VideoPlayer>();
                rawImage = gameObject.GetComponent<RawImage>();
                videoPlayer.Play();*/
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
        else if (other.tag == "Checkpoint" && estEnCourse == false)
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

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (rbVehicule.transform.forward.x <= 0 && estEnCourse == true)
        {
            estEnCourse = false;
        }
    }
}