using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum IADifficulty
{
    Facile,
    Normal,
    Difficile,
}

public class IA : MonoBehaviour
{
    NavMeshAgent agent;
    public float movementForce = 15;
    public Transform[] wayPoints;
    public IADifficulty iadifficulty;

    private int points;
    private new Rigidbody rigidbody;
    private IA IAscript;
    
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        IAscript = gameObject.GetComponent<IA>();
        agent.destination = wayPoints[0].position;
        
        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 35;
                break;
            
            case IADifficulty.Normal:
                agent.speed = 60;
                break;
            
            case IADifficulty.Difficile:
                agent.speed = 100;
                break;
        }
        print("vitesse : "+agent.speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        int bonusType =  Random.Range(0,4);
        if (other.CompareTag("BonusCube"))
        {
            if (bonusType == 0)
            {
                StartCoroutine(IAscript.Nitro());
                Debug.Log("Nitro");
            }

            if (bonusType == 1)
            {
                StartCoroutine(IAscript.SuperNitro());
                Debug.Log("SuperNitro");
            }

            if (bonusType == 2)
            {
                StartCoroutine(IAscript.Gravity());
                Debug.Log("SpeedMalus");
            }
            Destroy(other.gameObject);
        }
    }
    
    void Update()
    {
        float remainingDistance = agent.remainingDistance;
        if (remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            points = (points + 1) % wayPoints.Length;
            agent.destination = wayPoints[points].position;
            print(points);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(movementForce, 0f, 0f), ForceMode.Acceleration);
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
