using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public int nbTour = 3;
    private int nbTourRestant = 1;

    public Text tours;

    private void Start()
    {
        tours.text = nbTourRestant + "/" + nbTour;
    }

    void OnTriggerEnter(Collider collide)
    {

        if (collide.CompareTag("Finish"))
        {
            if (nbTourRestant >= nbTour)
            {
                Debug.Log("Finish");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else
            {
                nbTourRestant++;
                tours.text = nbTourRestant + "/" + nbTour;
            }
        }
    }
}
