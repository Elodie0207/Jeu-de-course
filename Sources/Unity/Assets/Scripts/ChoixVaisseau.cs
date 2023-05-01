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
        }
        else if (vaisseau == "BlueLagoon")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(true);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(false);
            Script.Vaisseau=BlueLagoon;
        }
        else if (vaisseau == "Ornitobeh")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(true);
            StarHess.SetActive(false);
            Script.Vaisseau=Ornitobeh;
        }
       
        else if (vaisseau == "StarHess")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(true);
            Script.Vaisseau=StarHess;
        }
        
    }

}
