using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class M_Chrono_Depart : MonoBehaviour
{
    public GameObject Count;
    public GameObject Vaisseau;

    public Text timer;
	private bool commencer=false; 
	private float Debut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Chrono());
        Vaisseau.GetComponent<MultiControl>().enabled=false;
        if (commencer==true){
		 
		}
    }

    IEnumerator Chrono(){
	    MultiControl[] CharList = FindObjectsOfType<MultiControl>();
        yield return new WaitForSeconds(0.3f);
        Count.SetActive(true);
    
       yield  return new WaitForSeconds(0.75f);
       Count.SetActive(true);

       yield return new WaitForSeconds(1);
       Count.SetActive(true);

        yield return new WaitForSeconds(1);
        Count.SetActive(true);

	    yield return new WaitForSeconds(1);
        Count.GetComponent<Text>().text="GO";
        Count.SetActive(false);
        foreach (MultiControl CharControl in CharList)
        {
	        CharControl.enabled = true;
        }
        //Vaisseau.GetComponent<MultiControl>().enabled=true;
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
