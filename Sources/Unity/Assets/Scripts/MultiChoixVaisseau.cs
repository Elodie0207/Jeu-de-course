using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChoixVaisseau : MonoBehaviour
{
    public GameObject Vaiseb;
    public GameObject BlueLagoon;
    public GameObject Ornitobeh;
    public GameObject StarHess;
    public M_Chrono_Depart Script;

    void Start()
    {
        string vaisseau2 = PlayerPrefs.GetString("vaisseau2");
        Debug.Log(vaisseau2);
        Vaiseb.SetActive(false); 
        BlueLagoon.SetActive(false);
        Ornitobeh.SetActive(false);
        StarHess.SetActive(false);

        if (vaisseau2 == "M_Vaiseb")
        {
            Vaiseb.SetActive(true); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(false);
            Script.Vaisseau=Vaiseb;
        }
        else if (vaisseau2 == "M_BlueLagoon")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(true);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(false);
            Script.Vaisseau=BlueLagoon;
        }
        else if (vaisseau2 == "M_Ornitobeh")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(true);
            StarHess.SetActive(false);
            Script.Vaisseau=Ornitobeh;
        }
       
        else if (vaisseau2 == "M_StarHess")
        {
            Vaiseb.SetActive(false); 
            BlueLagoon.SetActive(false);
            Ornitobeh.SetActive(false);
            StarHess.SetActive(true);
            Script.Vaisseau=StarHess;
        }
        
    }
}
