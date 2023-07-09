using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script pour split la camera en mode multijoueurs
public class SplitCamera : MonoBehaviour
{
    
    public Camera CamFirstPerson;
    public Camera CamThirdPerson;
    public Camera CamBack;
    
    //change le champs de vision de la camera
    void Start()
    {
        CamFirstPerson.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);
        
        CamThirdPerson.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);
        
        CamBack.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 1f);

    }
}
