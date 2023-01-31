using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collide){
     
        if(collide.tag=="Finish")
        {
            Debug.Log("Finish");    
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);    }
    }
}
