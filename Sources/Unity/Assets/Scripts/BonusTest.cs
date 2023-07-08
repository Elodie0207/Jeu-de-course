using UnityEngine;
using System.Collections;

public class BonusTest : MonoBehaviour
{
    public CharacterController playerCharacterController;
    public RaceManager Manager;
    private float RespawnTime = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 5);

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

            if (bonusType == 3)
            {
                StartCoroutine(playerCharacterController.Invert());
                Debug.Log("Inversion of Controls");
            }


            if (bonusType == 4)
            {

                StartCoroutine(Manager.Freeze());
                Debug.Log("Freeze of the First Player");
                
            }    

            
            other.gameObject.SetActive(false);

           
            StartCoroutine(ReactivateBonusCube(other.gameObject, RespawnTime));
        }
    }

   
    private IEnumerator ReactivateBonusCube(GameObject bonusCube, float count)
    {
        yield return new WaitForSeconds(count);
        bonusCube.SetActive(true);
    }
    
}