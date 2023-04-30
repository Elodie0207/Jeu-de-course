using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixVaisseau : MonoBehaviour
{
    public GameObject Vaiseb;
    public GameObject BlueLagoon;
    public GameObject Ornitobeh;
    public GameObject StarHess;
	public Chrono_Depart Script;
    //public GameObject Piste5;

    void Start()
    {
        string vaisseau = PlayerPrefs.GetString("vaisseau");
        Debug.Log(vaisseau);
        Vaiseb.SetActive(false); 
        BlueLagoon.SetActive(false);
        Ornitobeh.SetActive(false);
        StarHess.SetActive(false);

        if (vaisseau == "Vaiseb")
        {
            Vaiseb.SetActive(true); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(false);
			Script.Vaisseau=Vaiseb;
            //Piste5.SetActive(false);
        }
        else if (vaisseau == "BlueLagoon")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(true);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(false);
            Script.Vaisseau=BlueLagoon;
            // Piste5.SetActive(false);
        }
        else if (vaisseau == "Ornitobeh")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(true);
            StarHess.SetActive(false);
            Script.Vaisseau=Ornitobeh;
            //Piste5.SetActive(false);
        }
       
        else if (vaisseau == "StarHess")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(true);
            Script.Vaisseau=StarHess;
            //Piste5.SetActive(false);
        }
        /* else if (map == "Map5")
         {
             Vaiseb.SetActive(false); 
             BlueLagoon.SetActive(false);
             Ornitobeh.SetActive(false);
             StarHess.SetActive(false);
             Piste5.SetActive(true);
         }*/
    }

}
