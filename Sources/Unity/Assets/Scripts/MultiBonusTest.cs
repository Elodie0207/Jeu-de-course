using UnityEngine;
using System.Collections;

public class MultiBonusTest : MonoBehaviour
{
    public MultiControl MultiController;
    public RaceManager Manager;
    private float RespawnTime = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 4);

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

            if (bonusType == 3)
            {
                StartCoroutine(MultiController.Invert());
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