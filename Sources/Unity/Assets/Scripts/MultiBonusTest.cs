using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiBonusTest : MonoBehaviour
{
    public MultiControl MultiController;
    public RaceManager Manager;
    public Image nitroImage;
    public Image superNitroImage;
    public Image gravityImage;
    public Image inversionImage;
    public Image freezeImage;
    private float RespawnTime = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 5);

        if (other.CompareTag("BonusCube"))
        {
            Image bonusImage = null; 

            if (bonusType == 0)
            {
                StartCoroutine(MultiController.Nitro());
                bonusImage = nitroImage;
            }

            if (bonusType == 1)
            {
                StartCoroutine(MultiController.SuperNitro());
                bonusImage = superNitroImage;
            }

            if (bonusType == 2)
            {
                StartCoroutine(MultiController.Gravity());
                bonusImage = gravityImage;
            }

            if (bonusType == 3)
            {
                StartCoroutine(MultiController.Invert());
                bonusImage = inversionImage;
            }

            if (bonusType == 4)
            {
                StartCoroutine(Manager.Freeze());
                bonusImage = freezeImage;
            }    

            other.gameObject.SetActive(false);
            StartCoroutine(ReactivateBonusCube(other.gameObject, RespawnTime));

            // Activez l'image du bonus et la désactivez après 5 secondes
            StartCoroutine(ShowBonusImage(bonusImage, 5f));
        }
    }

   
    private IEnumerator ReactivateBonusCube(GameObject bonusCube, float count)
    {
        yield return new WaitForSeconds(count);
        bonusCube.SetActive(true);
    }
    
    private IEnumerator ShowBonusImage(Image bonusImage, float duration)
    {
        bonusImage.enabled = true;
        yield return new WaitForSeconds(duration);
        bonusImage.enabled = false; // Cache l'image du bonus après la durée spécifiée
    }
    
}