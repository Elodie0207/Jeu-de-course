using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class FinishScript : MonoBehaviour
{
    public GameObject tour;
    public Rigidbody rbVehicule;
    
    void OnTriggerEnter(Collider collide){
        if(collide.tag=="Finish" && rbVehicule.transform.forward.x > 0)
        {
            if (tour.GetComponent<Text>().text == "3")
            {
                Debug.Log("Finish");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

            }
            else if(tour.GetComponent<Text>().text == "0")
            {
                tour.GetComponent<Text>().text = "1";
            }
            else if(tour.GetComponent<Text>().text == "1")
            {
                tour.GetComponent<Text>().text = "2";
            }
            else if(tour.GetComponent<Text>().text == "2")
            {
                tour.GetComponent<Text>().text = "3";
            }
        }
        
        
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
            
        }
    }
}
