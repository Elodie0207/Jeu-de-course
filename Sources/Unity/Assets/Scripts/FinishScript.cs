using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FinishScript : MonoBehaviour
{
    [FormerlySerializedAs("nbTour")] public int nbTourRestant = 3;

    void OnTriggerEnter(Collider collide)
    {

        if (collide.tag == "Finish")
        {
            if (nbTourRestant <= 0)
            {
                Debug.Log("Finish");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else
            {
                nbTourRestant--;
            }
        }
    }
}
