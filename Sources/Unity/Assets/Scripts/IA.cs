using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//on détermine les difficultés des ia 
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
private float nearTurnDistance = 60f; // Distance pour considérer un virage proche
    private float nearTurnSpeed = 2f;
   void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
//on met en place la vitesse et le chemin choisi par l'ia, ils ont le choix entre deux chemins à suivre
        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 70;
                levelDifficulty = Random.Range(0, 2);
                break;

            case IADifficulty.Normal:
                agent.speed = 80;
                levelDifficulty = Random.Range(2, 4);
                break;

            case IADifficulty.Difficile:
                agent.speed = 90;
                levelDifficulty = Random.Range(4, 6);
                break;
        }

        agent.stoppingDistance = 0.5f;
        GotoNextPoint();
    }

//on permet aux ia de prendre les bonus
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BonusCube"))
        {
            int bonusType = Random.Range(0, 4);

            if (bonusType == 0)
            {
                StartCoroutine(Nitro());
             
            }
            else if (bonusType == 1)
            {
                StartCoroutine(SuperNitro());
              
            }
            else if (bonusType == 2)
            {
                StartCoroutine(Gravity());
               
            }

            Destroy(other.gameObject);
        }
    }

   private void Update()
{
  GameObject[] objs = GameObject.FindGameObjectsWithTag("Virage");
//on regarde la distance restante pour le prochain passage et on demande a l'ia de passer au suivant 
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        if (agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
    }
    
    if (agent.remainingDistance > 2f) // Vérifier si la distance restante est supérieure à une valeur seuil et on génère une rotation identique au joueur 
    {
        Vector3 movementDirection = agent.steeringTarget - transform.position;
        float angleToRotateVehicle = Mathf.Clamp(Vector3.SignedAngle(transform.forward, movementDirection, Vector3.up), -12f, 12f);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -angleToRotateVehicle);
        IAroot.localRotation = Quaternion.RotateTowards(IAroot.localRotation, targetRotation, maxDegreesRotation * Time.deltaTime);
    }
  float nearestDistance = float.MaxValue;
//On calcule la distance du joueur avec les prochains virages
    foreach (GameObject obj in objs)
    {
        float distance = Vector3.Distance(agent.transform.position, obj.transform.position);

        if (distance < nearestDistance)
        {
            nearestDistance = distance;
        }
    }
//on diminue la vitesse de l'ia a 15 pour lui permettre de passer par les virages
    if (nearestDistance < nearTurnDistance)
    {
        agent.speed = 15f;
    }
 else
    {
 //si il dépasse la distance donnée, il reprend sa vitesse initiale  
        switch (iadifficulty)
        {
            case IADifficulty.Facile:
                agent.speed = 70;
                break;
            case IADifficulty.Normal:
                agent.speed = 80;
                break;
            case IADifficulty.Difficile:
                agent.speed = 90;
                break;
        }
    }
}
//cette fonction permet a l'ia de suivre des waypoints situés dans un tableau 
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
//on ajoute les bonus que l'ia peut utiliser
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