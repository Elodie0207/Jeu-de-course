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
    private float maxDegreesRotation = 30f;
    private Quaternion targetRotation;
    private int points;
    private new Rigidbody rigidbody;
    private IA IAscript;
    private int levelDifficulty = 0;
    public float rotationSpeed = 5f;
    private List<Transform> way;

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
                agent.speed = 120;
                levelDifficulty = Random.Range(0, 2);
                break;

            case IADifficulty.Normal:
                agent.speed = 90;
                levelDifficulty = Random.Range(2, 4);
                break;

            case IADifficulty.Difficile:
                agent.speed = 100;
                levelDifficulty = Random.Range(4, 6);
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
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
            
}
        Vector3 movementDirection = agent.steeringTarget - transform.position;
        float angleToRotateVehicle = Mathf.Clamp(Vector3.SignedAngle(transform.forward, movementDirection, Vector3.up), -12f, 12f);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -angleToRotateVehicle);
        IAroot.localRotation = Quaternion.RotateTowards(IAroot.localRotation, targetRotation, maxDegreesRotation * Time.deltaTime);
    }

    void GotoNextPoint()
    {
        if (ways[levelDifficulty].wayPointsList.Count == 0)
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