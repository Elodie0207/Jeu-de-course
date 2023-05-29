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
    public bool cheat = false;
    public bool fin = false;
    public string map;

    GameObject piste1;
    GameObject piste2;
    GameObject piste3;
    GameObject piste4;
    GameObject piste5;
    public Image image;
	private Vector3 lastCheckpointPosition;
    void Start()
    {
        tour.GetComponent<Text>().text = "0";
        map = PlayerPrefs.GetString("map");
        checkpointsPasser = new bool[checkpoints.Length];
        ScoreBoard.enabled = false;
        Maps.GetComponent<ChoixMap>();
        piste1 = Maps.GetComponent<ChoixMap>().Piste1;
        piste2 = Maps.GetComponent<ChoixMap>().Piste2;
        piste3 = Maps.GetComponent<ChoixMap>().Piste3;
        piste4 = Maps.GetComponent<ChoixMap>().Piste4;
        piste5 = Maps.GetComponent<ChoixMap>().Piste5;
        image.gameObject.SetActive(false);
        cheat = false;
        nbTours = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && rbVehicule.transform.forward.x > 0 && nbcheckpointsPasser == checkpoints.Length)
        {
            nbTours++;

            if (nbTours == 3)
            {
                fin = true;
                Debug.Log("fin");
                tour.GetComponent<Text>().text = "0";
                nbTours = 0;

                RaceManager.UpdateScore();
                image.gameObject.SetActive(true);
                StartCoroutine(Wait()); // Démarrer une coroutine pour attendre 5 secondes
                //ScoreBoard.enabled=true;
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
                    Debug.Log("Checkpoint " + (i + 1));
                    checkpointsPasser[i] = true;
                    nbcheckpointsPasser++;
				lastCheckpointPosition = other.transform.position;
                    break;

                }
            }
        }
    }
	public Vector3 GetLastCheckpointPosition()
{
    return lastCheckpointPosition;
}
    void Update()
    {
        cheat = FindObjectOfType<Autres>().cheatcode;
        Debug.Log(PlayerPrefs.GetString("map"));
    }
    private IEnumerator WaitForNextScene()
    {
        // Attendre 5 secondes
        yield
        return new WaitForSeconds(waitTime);

        // Passer à la ligne suivante
        ScoreBoard.enabled = false;

        if (PlayerPrefs.GetString("map") == "Map1")
        {
            cheat = false;
            PlayerPrefs.SetString("map", "Map2");
            map = PlayerPrefs.GetString("map");
            tour.GetComponent<Text>().text = "0";
            nbTours = 0;
            piste1.SetActive(false);
            piste2.SetActive(true);
            piste3.SetActive(false);
            piste4.SetActive(false);
            piste5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("map") == "Map2")
        {
            cheat = false;
            PlayerPrefs.SetString("map", "Map3");
            map = PlayerPrefs.GetString("map");
            tour.GetComponent<Text>().text = "0";
            nbTours = 0;
            piste1.SetActive(false);
            piste2.SetActive(false);
            piste3.SetActive(true);
            piste4.SetActive(false);
            piste5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("map") == "Map3")
        {
            cheat = false;

            PlayerPrefs.SetString("map", "Map4");
            map = PlayerPrefs.GetString("map");
            tour.GetComponent<Text>().text = "0";
            nbTours = 0;
            piste1.SetActive(false);
            piste2.SetActive(false);
            piste3.SetActive(false);
            piste4.SetActive(true);
            piste5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("map") == "Map4")
        {
            cheat = false;
            PlayerPrefs.SetString("map", "Map5");
            map = PlayerPrefs.GetString("map");
            tour.GetComponent<Text>().text = "0";
            nbTours = 0;
            piste1.SetActive(false);
            piste2.SetActive(false);
            piste3.SetActive(false);
            piste4.SetActive(false);
            piste5.SetActive(true);
        }
        else if (PlayerPrefs.GetString("map") == "Map5")
        {
            cheat = false;
            map = "fin";
            tour.GetComponent<Text>().text = "0";
            nbTours = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private IEnumerator Wait()
    {
        // Attendre 5 secondes
        yield
        return new WaitForSeconds(waitTime);

        // Passer à la ligne suivante
        ScoreBoard.enabled = true;
        image.gameObject.SetActive(false);

        StartCoroutine(WaitForNextScene());

    }

}