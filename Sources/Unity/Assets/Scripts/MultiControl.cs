using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiControl : MonoBehaviour
{ 
	public float yawRotationSpeed;
	public float movementForce = 15; // vitesse
	public Transform feetTransform;
	public Rigidbody rb;
	public Transform playerModelAndCamera;

	public float maxDegreesRotation = 100f;
	public float angleToRotateVehicle = 30f;
	
	public float levitationHeight = 5.0f;
	public float levitationForce = 20.0f;
	public LayerMask roadLayer;

	public GameObject trComplet;
	private float startY;

	private RaycastHit hit;
	private bool levitationBool = true;
	
	private bool isRotatingLeft;
	private bool isRotatingRight;
	private Vector3 movementIntent;


	void Start()
	{
		rb.useGravity = false;
		startY = transform.position.y;
		trComplet.SetActive(true);
	}

    
    void Update()
    {
	    float yPosition = feetTransform.position.y;
		movementIntent = Vector3.zero;
		if (yPosition < -10)
		{
			Vector3 newPosition = new Vector3(-44.73f, 6.17f, 37.9f);

			
			feetTransform.position = newPosition;
		}
		
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow))
        {
			movementIntent += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow))
        {
			movementIntent += Vector3.back;
        }

        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
        {
			isRotatingLeft = true;
	        movementIntent += Vector3.left;
        }
        else
        {
	        isRotatingLeft = false;
        }
        
        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
        {
	        isRotatingRight = true;
	        movementIntent += Vector3.right;
        }
        else
        {
	        isRotatingRight = false;
        }
        movementIntent = movementIntent.normalized;
	}

    private void FixedUpdate()
    {
	    if (isRotatingLeft)
	    {
		    var rotation = feetTransform.rotation;
		    rotation = Quaternion.Euler(
			    rotation.eulerAngles.x,
			    rotation.eulerAngles.y - yawRotationSpeed * Time.deltaTime,
			    rotation.eulerAngles.z);
		    feetTransform.rotation = rotation;
		    var localRotation = playerModelAndCamera.localRotation;
		    localRotation = Quaternion.RotateTowards(localRotation, Quaternion.Euler(0f, 0f, angleToRotateVehicle), maxDegreesRotation * Time.deltaTime);
		    playerModelAndCamera.localRotation = localRotation;
	    }
	    if (isRotatingRight)
	    {
		    var rotation = feetTransform.rotation;
		    rotation = Quaternion.Euler(
			    rotation.eulerAngles.x,
			    rotation.eulerAngles.y + yawRotationSpeed * Time.deltaTime,
			    rotation.eulerAngles.z);
		    feetTransform.rotation = rotation;
		    var localRotation = playerModelAndCamera.localRotation;
		    localRotation = Quaternion.RotateTowards(localRotation, Quaternion.Euler(0f, 0f, -angleToRotateVehicle), maxDegreesRotation * Time.deltaTime);
		    playerModelAndCamera.localRotation = localRotation;
	    }

	    if (!isRotatingLeft && !isRotatingRight)
	    {
		    var localRotation = playerModelAndCamera.localRotation;
		    localRotation = Quaternion.RotateTowards(localRotation, Quaternion.Euler(0f, 0f, 0f), maxDegreesRotation * Time.deltaTime);
		    playerModelAndCamera.localRotation = localRotation;
	    }

        rb.AddForce(movementForce * (feetTransform.rotation * movementIntent), ForceMode.Acceleration);
        
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, roadLayer)) {
	        Vector3 levitationPos = hit.point + Vector3.up * levitationHeight;
	        transform.position = levitationPos;
	        
	        if (levitationBool) {
		        rb.AddForce(Vector3.up * levitationForce);
	        }
        }
        else {
	        levitationBool = false;
	        rb.useGravity = true;
        }
    }
    
    public IEnumerator Nitro(float count = 5f)
    {

	    movementForce = 250;

	    yield return new WaitForSeconds(count);
	    
	    movementForce = 170;

    }
    
    public IEnumerator SuperNitro(float count = 8f)
    {
	    
	    movementForce = 250;
		
	    yield return new WaitForSeconds(count);

	    movementForce = 170;
	    
    }
    
    public IEnumerator Gravity(float count = 5f)
    {
	    movementForce = 75;
	    
	    yield return new WaitForSeconds(count);
	    
	    movementForce = 170;
	   
    }
}


    
    
    


