using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Debug.Log(map);
       
       	Piste1.SetActive(false); 
        Piste2.SetActive(true);
        Piste3.SetActive(false);
        Piste4.SetActive(false);
       Piste5.SetActive(false);

       
    }

    
    void Update()
    {
        
    }
}
