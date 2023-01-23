using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class CharacterController : MonoBehaviour
{ 
	public float yawRotationSpeed;
	public float movementForce = 15; // vitesse
	public Transform feetTransform;
	private Vector3 movementIntent;
	public Rigidbody rb;
	
    void Start()
    {

	

    }

   
    
    void Update()
    {
		movementIntent = Vector3.zero;
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
            rotation = Quaternion.Euler(
            	rotation.eulerAngles.x,
                rotation.eulerAngles.y - yawRotationSpeed * Time.deltaTime,
                rotation.eulerAngles.z);
			feetTransform.rotation = rotation;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var rotation = feetTransform.rotation;
            rotation = Quaternion.Euler(
            	rotation.eulerAngles.x,
                rotation.eulerAngles.y + yawRotationSpeed * Time.deltaTime,
                rotation.eulerAngles.z);
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