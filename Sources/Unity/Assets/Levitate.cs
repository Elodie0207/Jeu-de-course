using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float speed = 0.2f;
    public bool isRandom = false;

    private float startPosY;
    private Vector3 Position;
    
    void Start()
    {
        startPosY = transform.position.y;
        if (isRandom == true) //pr eviter syncro
        {
            amplitude = Random.Range(amplitude, amplitude + 2f);
        }
    }
    
    void Update()
    {
        Position.y = startPosY + amplitude * Mathf.Sin(speed + Time.time);
        Position.x = transform.position.x;
        Position.z = transform.position.z;
        transform.position = Position;
    }
}
