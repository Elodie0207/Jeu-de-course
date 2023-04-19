using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HoverV5 : MonoBehaviour

{
    // Start is called before the first frame update

    public GameObject txtVaisseau; 
   
    void Start()
    {
        txtVaisseau.SetActive(false);
    }

    public void OnPointerEnter()
    {
        txtVaisseau.SetActive(true);
    }

    public void OnPointerExit()
    {
        txtVaisseau.SetActive(false);
    }
}
