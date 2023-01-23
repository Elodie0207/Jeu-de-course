using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collide){
     
        if(collide.tag=="Finish")
        {
            Debug.Log("Finish");        }
    }
}
