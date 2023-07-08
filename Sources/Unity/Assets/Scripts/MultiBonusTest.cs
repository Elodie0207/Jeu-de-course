using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiBonusTest : MonoBehaviour
{
    public MultiControl MultiController;
    public RaceManager Manager;
    public Text bonusNameText; 
    private float RespawnTime = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 4);

        if (other.CompareTag("BonusCube"))
        {
            string bonusName = ""; // Nous allons utiliser cette variable pour stocker le nom du bonus

            if (bonusType == 0)
            {
                StartCoroutine(MultiController.Nitro());
                
                bonusName = "Nitro";
            }

            if (bonusType == 1)
            {
                StartCoroutine(MultiController.SuperNitro());
                bonusName = "SuperNitro";
            }

            if (bonusType == 2)
            {
                StartCoroutine(MultiController.Gravity());
                bonusName = "Gravity";
            }

            if (bonusType == 3)
            {
                StartCoroutine(MultiController.Invert());
                bonusName = "Inversion";
            }


            if (bonusType == 4)
            {
                StartCoroutine(Manager.Freeze());
                bonusName = "Freeze";
            }    

            other.gameObject.SetActive(false);
            StartCoroutine(ReactivateBonusCube(other.gameObject, RespawnTime));

            // Affichez le nom du bonus à l'écran et le cache après 5 secondes
            StartCoroutine(ShowBonusName(bonusName, 5f));
        }
    }

   
    private IEnumerator ReactivateBonusCube(GameObject bonusCube, float count)
    {
        yield return new WaitForSeconds(count);
        bonusCube.SetActive(true);
    }
    
    private IEnumerator ShowBonusName(string bonusName, float count)
    {
        bonusNameText.enabled = true;
        bonusNameText.text = bonusName;
        yield return new WaitForSeconds(count);
        bonusNameText.enabled = false;
    }
    
}