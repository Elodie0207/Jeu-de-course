using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Chrono_Depart : MonoBehaviour
{
    public GameObject Count;
    public GameObject Vaisseau; 
    public GameObject IA1;
	public GameObject IA2;
	public GameObject IA3;
	
	 public Text timer;
	private bool commencer=false; 
	private float Debut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Chrono());
        Vaisseau.GetComponent<CharacterController>().enabled=false;
        IA1.GetComponent<NavMeshAgent>().enabled=false;
        IA2.GetComponent<NavMeshAgent>().enabled=false;
        IA3.GetComponent<NavMeshAgent>().enabled=false;
 		if (commencer==true){
		 
		}
    }

    IEnumerator Chrono(){
	    
	    List<NavMeshAgent> navMeshAgents = new List<NavMeshAgent>(FindObjectsOfType<NavMeshAgent>());
	    CharacterController[] CharList = FindObjectsOfType<CharacterController>();
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
        foreach (CharacterController CharControl in CharList)
        {
	        CharControl.enabled = true;
        }
        
        foreach (NavMeshAgent agent in navMeshAgents)
        {
	        agent.enabled = true;
        }
        //Vaisseau.GetComponent<CharacterController>().enabled=true;
		/*IA1.GetComponent<NavMeshAgent>().enabled=true;
		IA2.GetComponent<NavMeshAgent>().enabled=true;
		IA3.GetComponent<NavMeshAgent>().enabled=true;*/
		if(Count.GetComponent<Text>().text=="GO"){
		commencer=true; 
		Debut = Time.time;
}
       
        //Vaisseau.SetActive(true);
    }
    public void ResetChrono()
    {
	    Debut = Time.time;
	    commencer = true;
	    StartCoroutine(Chrono());
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
