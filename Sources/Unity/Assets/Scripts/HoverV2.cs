using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverV2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject txtVaisseau; 
   
    void Start()
    {
        txtVaisseau.SetActive(false);
    }

    public void OnMouseOver()
    {
        txtVaisseau.SetActive(true);
    }
    // Update is called once per frame
    public void OnMouseExit()
    {
        txtVaisseau.SetActive(false);
    }
}
