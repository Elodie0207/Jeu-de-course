using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    
    public GameObject object1;
    //public GameObject object2;
    
    public Vector3 object1Position;
    //private Vector3 object2Position;
    

    // Update is called once per frame
    void Update()
    {
        object1Position = object1.transform.position;
        //object2Position = object2.transform.position;

        Debug.Log("Object 1 position: " + object1Position);
        //Debug.Log("Object 2 position: " + object2Position);
        
    }
}
