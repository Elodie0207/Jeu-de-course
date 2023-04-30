using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum IADifficulty
{
    Facile,
    Normal,
    Difficile,
}

[System.Serializable]
public class WayPointList
{
    public List<Transform> wayPointsList = new List<Transform>();
}

public class IA : MonoBehaviour
{
    NavMeshAgent agent;
    public float movementForce = 15;
    
    //public Transform[] wayPoints;
    public IADifficulty iadifficulty;
    [SerializeField] private List<WayPointList> ways;
    public Transform IAroot;
    
    private int points;
    private new Rigidbody rigidbody;
    private IA IAscript;
    private int levelDifficulty = 0;

    private List<Transform> way;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        IAscript = gameObject.GetComponent<IA>();

        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 35;
                
                levelDifficulty = Random.Range(0, 2);
                break;
            
            case IADifficulty.Normal:
                agent.speed = 60;
                levelDifficulty = Random.Range(2, 4);
                break;
            
            case IADifficulty.Difficile:
                agent.speed = 100;
                levelDifficulty  = Random.Range(4, 6);
                break;
        }
        //print("level :"+levelDifficulty);
        agent.destination = ways[levelDifficulty].wayPointsList[0].position;
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
        if (agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
        
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(movementForce, 0f, 0f), ForceMode.Acceleration);
    }

    void GotoNextPoint()
    {
        if (ways[levelDifficulty].wayPointsList.Count == 0)
            return;
        
        points = (points + 1) % ways[levelDifficulty].wayPointsList.Count;
        //print(points);
        
        agent.destination = ways[levelDifficulty].wayPointsList[points].position;
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
