using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteRebours : MonoBehaviour
{
     public float speed = 10020;
    public float rotationSpeed = 50.0f;
    public float tiltAmount = 15.0f;
    public float smoothAmount = 0.5f;
    public Rigidbody motorcycleRigidbody;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 tiltDirection = Vector3.zero;
    private float tiltVelocity;
    private float moveVelocity;
    private float currentRotation;

    void Start()
    {
        motorcycleRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        tiltDirection = new Vector3(horizontalInput, 0, verticalInput);
        tiltDirection = transform.TransformDirection(tiltDirection);
        tiltDirection *= tiltAmount;

        motorcycleRigidbody.AddForce(transform.forward * verticalInput * speed);
        motorcycleRigidbody.rotation = Quaternion.Euler(tiltDirection);

        
    }
}
