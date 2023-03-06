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
	private float maxAngle = 170f;
	void Start()
	{
		rb.useGravity = false;
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
    
    private void OnTriggerStay(Collider other)
    {
	    if (other.CompareTag("Player"))
	    {
		    // Vérifier l'angle de rotation du joueur
		    float angle = Vector3.Angle(Vector3.forward, other.transform.forward);
		    if (angle > maxAngle)
		    {
			    // Empêcher le joueur de tourner à 180 degrés
			    Vector3 newForward = Vector3.RotateTowards(other.transform.forward, Vector3.forward, maxAngle * Mathf.Deg2Rad, 0f);
			    other.transform.rotation = Quaternion.LookRotation(newForward);
		    }
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