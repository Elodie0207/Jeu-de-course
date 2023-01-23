using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    NavMeshAgent agent;
    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GetDestination();
    
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            agent.destination = GetDestination();
        }
    }

    public Vector3 GetDestination()
    {
        //int destination = Random.Range(0, 4);
        return GameObject.Find("Pts" + 2).transform.position;
    }
}
