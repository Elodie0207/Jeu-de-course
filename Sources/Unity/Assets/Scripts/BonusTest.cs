
using UnityEngine;

public class BonusTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        int bonusType =  UnityEngine.Random.Range(0,2);

        
        
        if (other.CompareTag("BonusCube"))
        {
            
        
            if (bonusType == 0)
            {
                
                    Debug.Log("Non");
                   
                   

            }

            if (bonusType == 1)
            {
                    
                    Debug.Log("Oui");
                   
            }
       
            Destroy(other.gameObject);
        }
        
        
        
    }

    
    }
    
