
using UnityEngine;


public class MultiBonusTest : MonoBehaviour
{
    public MultiControl MultiController;

    //public Bullet bulletProj;
    
    private void OnTriggerEnter(Collider other)
    {   
        //création d'une valeur aléatoire comprise entre 0 et 4;
        
        int bonusType =  UnityEngine.Random.Range(0,4);

        
        //un asset qui correspond au cube de bonus a pour tag bonus cube, si le joueur rentre en collision avec en fonction de la valeur de bonusType un bonus ou un malus sera donné au joueur.
        if (other.CompareTag("BonusCube"))
        {
            
        
            if (bonusType == 0)
            {

                StartCoroutine(MultiController.Nitro());
                Debug.Log("Nitro");



            }

            if (bonusType == 1)
            {
                    
                StartCoroutine(MultiController.SuperNitro());
                Debug.Log("SuperNitro");

                   
            }

          if (bonusType == 2)
            {

                StartCoroutine(MultiController.Gravity());
                Debug.Log("SpeedMalus");



            }   
            
            /* if (bonusType == 3)
            {

                StartCoroutine(bulletProj.Shoot());
                Debug.Log("Bullet");



            } */ 
                
            //destruction du cube pour qu'il ne soit pas possible de reprendre le bonus juste après
            Destroy(other.gameObject);
        }
        
        
        
    }

    
    }
    
    
