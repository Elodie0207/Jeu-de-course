using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixMap : MonoBehaviour
{
    public GameObject Piste1;
    public GameObject Piste2;
    public GameObject Piste3;
    public GameObject Piste4;
    //public GameObject Piste5;

    void Start()
    {
        string map = PlayerPrefs.GetString("map");
        Debug.Log(map);
        Piste1.SetActive(true); 
        Piste2.SetActive(false);
        Piste3.SetActive(false);
        Piste4.SetActive(false);
       /*if (map == "Map1")
        {
            Piste1.SetActive(true); 
            Piste2.SetActive(false);
            Piste3.SetActive(false);
            Piste4.SetActive(false);
            //Piste5.SetActive(false);
        }
        else if (map == "Map2")
        {
            Piste1.SetActive(false); 
            Piste2.SetActive(true);
            Piste3.SetActive(false);
            Piste4.SetActive(false);
            // Piste5.SetActive(false);
        }
        else if (map == "Map3")
        {
            Piste1.SetActive(false); 
            Piste2.SetActive(false);
           Piste3.SetActive(true);
           Piste4.SetActive(false);
          //Piste5.SetActive(false);
        }
       
        else if (map == "Map4")
        {
            Piste1.SetActive(false); 
            Piste2.SetActive(false);
            Piste3.SetActive(false);
            Piste4.SetActive(true);
            //Piste5.SetActive(false);
        }
        /* else if (map == "Map5")
         {
             Piste1.SetActive(false); 
             Piste2.SetActive(false);
             Piste3.SetActive(false);
             Piste4.SetActive(false);
             Piste5.SetActive(true);
         }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
