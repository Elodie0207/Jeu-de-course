using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSplitCamera : MonoBehaviour
{
    
    public Camera CamFirstPerson;
    public Camera CamThirdPerson;
    public Camera CamBack;
    
    // Start is called before the first frame update
    void Start()
    {
        CamFirstPerson.GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 1f);
        
        CamThirdPerson.GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 1f);
        
        CamBack.GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 1f);
    
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}