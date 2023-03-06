using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Chrono_Depart : MonoBehaviour
{
    public GameObject Count;
  
    public GameObject Vaisseau;
	public GameObject IA;
	 public Text timer;
	private bool commencer=false; 
private float Debut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Chrono());
        Vaisseau.GetComponent<CharacterController>().enabled=false;
        IA.GetComponent<NavMeshAgent>().speed=0;
 		if (commencer==true){
		 
		}
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
        Count.GetComponent<Text>().text="GO";
        Count.SetActive(true);

          yield return new WaitForSeconds(1);
        Count.GetComponent<Text>().text="GO";
        Count.SetActive(false);
        Vaisseau.GetComponent<CharacterController>().enabled=true;
		IA.GetComponent<NavMeshAgent>().speed=25;
		if(Count.GetComponent<Text>().text=="GO"){
		 commencer=true; 
Debut = Time.time;
}
       
        //Vaisseau.SetActive(true);
    }

	void Update(){
	if(commencer==true){
		
	float time = Time.time - Debut;
        string secondes = (time % 60).ToString("f1");
        string minutes = ((int)time / 60).ToString();
        timer.text = minutes + ":" + secondes;
}
}
}
