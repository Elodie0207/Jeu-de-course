using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BonusTest : MonoBehaviour
{
    public CharacterController playerCharacterController;
    public RaceManager Manager;
    public Text bonusNameText; 
    private float RespawnTime = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 5);

        if (other.CompareTag("BonusCube"))
        {
            string bonusName = ""; 

            if (bonusType == 0)
            {
                StartCoroutine(playerCharacterController.Nitro());
                
                bonusName = "Nitro";
            }

            if (bonusType == 1)
            {
                StartCoroutine(playerCharacterController.SuperNitro());
                bonusName = "SuperNitro";
            }

            if (bonusType == 2)
            {
                StartCoroutine(playerCharacterController.Gravity());
                bonusName = "Gravity";
            }

            if (bonusType == 3)
            {
                StartCoroutine(playerCharacterController.Invert());
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
