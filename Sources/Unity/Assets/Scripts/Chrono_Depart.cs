using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chrono_Depart : MonoBehaviour
{
    public GameObject Count;
    public GameObject Départ;
    public GameObject Go; 
    public GameObject Vaisseau;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Chrono());
        Vaisseau.GetComponent<CharacterController>().enabled=false;
        
    }

    IEnumerator Chrono(){
        yield return new WaitForSeconds(0.3f);
        Count.GetComponent<Text>().text="3";
        Count.SetActive(true);
    
       yield  return new WaitForSeconds(0.75f);
        Count.GetComponent<Text>().text="2";
        Count.SetActive(true);

       yield return new WaitForSeconds(1);
        Count.GetComponent<Text>().text="1";
        Count.SetActive(true);

        yield return new WaitForSeconds(1);
        Count.GetComponent<Text>().text="PARTEZ";
        Count.SetActive(true);

          yield return new WaitForSeconds(1);
        Count.GetComponent<Text>().text="PARTEZ";
        Count.SetActive(false);
         Vaisseau.GetComponent<CharacterController>().enabled=true;
        //Vaisseau.SetActive(true);
    }
}
