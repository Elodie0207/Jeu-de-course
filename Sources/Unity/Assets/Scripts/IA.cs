using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform move; 
    public Transform next;
    private int nbTour = 0;
    private bool canMove = false;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
 
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Points"))
        {
            canMove = true;
        }
    }
    void Update()
    {
        if (canMove == false)
        {
            agent.destination = move.position;
        }
        else
        {
            agent.destination = next.position;
        }



    }
    
}
