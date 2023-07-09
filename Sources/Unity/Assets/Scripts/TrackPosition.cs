using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script pour suivre la position du gameObject
public class TrackPosition : MonoBehaviour
{
    //gameObject dont on veut connaitre la pos
    public GameObject object1;
    
    public Vector3 object1Position;
    

    void Update()
    {
        object1Position = object1.transform.position;
        
    }
}
