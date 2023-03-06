using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform move; 
    public Transform next;
    public Transform last;

    private int points = 0;

    
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Points"))
        {
            if (points < 3)
            {
                points += 1;
            }
           
        }
       
        
    }
    void Update()
    {
        print(points);
        print(agent.destination);
        if (points == 3)
        {
            points = 0;
            agent.destination = move.position;
        }
        else if (points==0)
        {
            agent.destination = move.position;
        }
        else if  (points==1)
        {
            agent.destination = next.position;
            
        }
        else if (points==2){
            agent.destination = last.position;
        }



    }
    
}
