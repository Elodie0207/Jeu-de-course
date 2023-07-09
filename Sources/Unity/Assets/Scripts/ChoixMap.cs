using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script pour choisir les maps
public class ChoixMap : MonoBehaviour
{
    public GameObject Piste1;
    public GameObject Piste2;
    public GameObject Piste3;
    public GameObject Piste4;
	public GameObject Piste5;

    void Start()
    {

        string map = PlayerPrefs.GetString("map");
       
        //met par defaut la map 1
       	Piste1.SetActive(true); 
        Piste2.SetActive(false);
        Piste3.SetActive(false);
        Piste4.SetActive(false);
        Piste5.SetActive(false);

       
    }
}
