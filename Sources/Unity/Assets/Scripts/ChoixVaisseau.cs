using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixVaisseau : MonoBehaviour
{
    public GameObject Vaisseau1;
    public GameObject Vaisseau2;
    public GameObject Vaisseau3;
    public GameObject Vaisseau4;
    //public GameObject Piste5;

    void Start()
    {
        string vaisseau = PlayerPrefs.GetString("vaisseau");
        Debug.Log(vaisseau);
        Vaisseau1.SetActive(false); 
        Vaisseau2.SetActive(false);
        Vaisseau3.SetActive(false);
        Vaisseau4.SetActive(false);
        if (vaisseau == "Vaiseb")
        {
            Vaisseau1.SetActive(true); 
            Vaisseau2.SetActive(false);
            Vaisseau3.SetActive(false);
            Vaisseau4.SetActive(false);
            //Piste5.SetActive(false);
        }
        else if (vaisseau == "BlueLagoon")
        {
            Vaisseau1.SetActive(false); 
            Vaisseau2.SetActive(false);
            Vaisseau3.SetActive(true);
            Vaisseau4.SetActive(false);
            // Piste5.SetActive(false);
        }
        else if (vaisseau == "Ornitobeh")
        {
            Vaisseau1.SetActive(false); 
            Vaisseau2.SetActive(false);
            Vaisseau3.SetActive(false);
            Vaisseau4.SetActive(true);
            //Piste5.SetActive(false);
        }
       
        else if (vaisseau == "StarHess")
        {
            Vaisseau1.SetActive(false); 
            Vaisseau2.SetActive(true);
            Vaisseau3.SetActive(false);
            Vaisseau4.SetActive(false);
            //Piste5.SetActive(false);
        }
        /* else if (map == "Map5")
         {
             Vaisseau1.SetActive(false); 
             Vaisseau2.SetActive(false);
             Vaisseau3.SetActive(false);
             Vaisseau4.SetActive(false);
             Piste5.SetActive(true);
         }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
