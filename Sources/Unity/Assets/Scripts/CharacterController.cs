using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{ 
	public float yawRotationSpeed;
	public float movementForce = 15; // vitesse
	public Transform feetTransform;
	private Vector3 movementIntent;
	public Rigidbody rb;
	
	public float levitationHeight = 5.0f;
	public float levitationForce = 20.0f;
	public LayerMask roadLayer;
	private RaycastHit hit;
	private bool levitationBool = true;


	void Start()
	{
		rb.useGravity = false;
		

	}

    
    void Update()
    {
	    float yPosition = feetTransform.position.y;
	    float xPosition = feetTransform.position.x;
		movementIntent = Vector3.zero;
		if (yPosition < 0)
		{
			Vector3 newPosition = new Vector3(-44.73f, 6.17f, 37.9f);

			
			feetTransform.position = newPosition;
		}
		
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
			movementIntent += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
			movementIntent += Vector3.back;
        }
        
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
	        
            var rotation = feetTransform.rotation;
            /*
            rotation = Quaternion.Euler(
            	rotation.eulerAngles.x,
                rotation.eulerAngles.y - yawRotationSpeed * Time.deltaTime,
                rotation.eulerAngles.z);*/
            rotation = Quaternion.Euler(
	            rotation.eulerAngles.x,
	            rotation.eulerAngles.y - yawRotationSpeed * Time.deltaTime,
	            (float)(10.5));
			feetTransform.rotation = rotation;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var rotation = feetTransform.rotation;
            rotation = Quaternion.Euler(
            	rotation.eulerAngles.x,
                rotation.eulerAngles.y + yawRotationSpeed * Time.deltaTime,
                (float)(-10.5));
			feetTransform.rotation = rotation;
        }
	movementIntent = movementIntent.normalized;
	}

    private void FixedUpdate()
    {
		//teste pr eculer + lent que avancer
		/*if (Input.GetKey(KeyCode.S))
		{
			if (movementForce > 6)
				{
					rb.AddForce((movementForce - 5) * (feetTransform.rotation * movementIntent), ForceMode.Acceleration);
				}
		}
		else
		{
			rb.AddForce(movementForce * (feetTransform.rotation * movementIntent), ForceMode.Acceleration);
		}*/
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

	    movementForce *= 2f;

	    yield return new WaitForSeconds(count);
	    
	    movementForce *= 0.5f;

    }
    
    public IEnumerator SuperNitro(float count = 10f)
    {
	    
	    movementForce *= 2f;
		
	    yield return new WaitForSeconds(count);

	    movementForce *= 0.5f;
	    
    }
    
    public IEnumerator Gravity(float count = 5f)
    {
	    movementForce *= 0.5f;
	    
	    yield return new WaitForSeconds(count);
	    
	    movementForce *= 2f;
	   
    }


    
    
    


}