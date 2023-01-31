
using UnityEngine;


public class BonusTest : MonoBehaviour
{
    public CharacterController playerCharacterController;

    //public Bullet bulletProj;
    
    private void OnTriggerEnter(Collider other)
    {
        int bonusType =  UnityEngine.Random.Range(0,4);

        
        
        if (other.CompareTag("BonusCube"))
        {
            
        
            if (bonusType == 0)
            {

                StartCoroutine(playerCharacterController.Nitro());
                Debug.Log("Nitro");



            }

            if (bonusType == 1)
            {
                    
                StartCoroutine(playerCharacterController.SuperNitro());
                Debug.Log("SuperNitro");

                   
            }

          if (bonusType == 2)
            {

                StartCoroutine(playerCharacterController.Gravity());
                Debug.Log("SpeedMalus");



            }   
            
            /* if (bonusType == 3)
            {

                StartCoroutine(bulletProj.Shoot());
                Debug.Log("Bullet");



            } */ 
                
       
            Destroy(other.gameObject);
        }
        
        
        
    }

    
    }
    
    
