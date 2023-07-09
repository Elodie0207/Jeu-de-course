using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script pour faire un timer
public class Timer : MonoBehaviour
{
    public Text timer;

    private float Debut;
    void Start()
    {
        Debut = Time.time;
    }
    

    // Update is called once per frame
    void Update()
    {
        float time = Time.time - Debut;
        string secondes = (time % 60).ToString("f1");
        string minutes = ((int)time / 60).ToString();
        timer.text = minutes + ":" + secondes;
        
    }
}

