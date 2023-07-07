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

    public IADifficulty iadifficulty;
    [SerializeField] private List<WayPointList> ways;
    public Transform IAroot;
    private float maxDegreesRotation = 50f;
    private Quaternion targetRotation;
    private int points;
    private new Rigidbody rigidbody;
    private IA IAscript;
    private int levelDifficulty = 0;
    public float rotationSpeed = 500f;
    private List<Transform> way;
	private float nearTurnDistance = 60f; // Distance pour considérer un virage proche
    private float nearTurnSpeed = 2f;
	
   void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

       private void Start()
    {
        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 50;
                levelDifficulty = 1;
                break;

            case IADifficulty.Normal:
                agent.speed = 60;
                levelDifficulty = Random.Range(2, 3);
                break;

            case IADifficulty.Difficile:
                agent.speed = 70;
                levelDifficulty = 5;
                break;
        }

        agent.stoppingDistance = 0.5f;
        GotoNextPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BonusCube"))
        {
            int bonusType = Random.Range(0, 4);

            if (bonusType == 0)
            {
                StartCoroutine(Nitro());
                Debug.Log("Nitro");
            }
            else if (bonusType == 1)
            {
                StartCoroutine(SuperNitro());
                Debug.Log("SuperNitro");
            }
            else if (bonusType == 2)
            {
                StartCoroutine(Gravity());
                Debug.Log("Gravity");
            }

            Destroy(other.gameObject);
        }
    }

  private void Update()
{
    GameObject[] objs = GameObject.FindGameObjectsWithTag("Virage");

    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        if (agent.remainingDistance < 5f)
        {
            GotoNextPoint();
        }
    }

   float rotationStep = rotationSpeed * Time.deltaTime;
Vector3 targetDirection = agent.steeringTarget - transform.position;
Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationStep);

    float nearestDistance = float.MaxValue;
    foreach (GameObject obj in objs)
    {
        float distance = Vector3.Distance(agent.transform.position, obj.transform.position);

        if (distance < nearestDistance)
        {
            nearestDistance = distance;
        }
    }

    if (nearestDistance < nearTurnDistance)
    {
        agent.speed = 15f;
    }
    else
    {
        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 50;
                break;
            case IADifficulty.Normal:
                agent.speed = 60;
                break;
            case IADifficulty.Difficile:
                agent.speed = 70;
                break;
        }
    }
}

  void GotoNextPoint()
{
    if (ways == null || levelDifficulty >= ways.Count || ways[levelDifficulty] == null || ways[levelDifficulty].wayPointsList.Count == 0)
        return;

    points++;
    if (points >= ways[levelDifficulty].wayPointsList.Count)
    {
        points = 0;
    }

    Vector3 correctedPosition = ways[levelDifficulty].wayPointsList[points].position;
    agent.SetDestination(correctedPosition);
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